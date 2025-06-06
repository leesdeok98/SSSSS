using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] AudioMixerGroup audioMixer;
    [SerializeField] AudioMixerGroup BGMMixerGroup;
    [SerializeField] AudioMixerGroup SFXMixerGroup;

    public AudioClip MainMusic; // 메인 타이틀 음악    
    public AudioClip Chapter1; // 챕터 1 음악
    public AudioClip Chapter2; // 챕터 2 음악
    public AudioClip Chapter3; // 챕터 3 음악

    [Header("#BGM)")]
    public AudioClip[] BGMClips;
    public float BGMVoulme;
    AudioSource BGMPlayer;


    /* [Header("#SFX)")]
    public AudioClip[] SFXClips;
    public float SFXVolume;
    public int channels;
    AudioSource[] SFXPlayers;
    int channelIndex;
    private Sound[] sounds;

    public enum SFX { BA, BAp, BD, BL, Dead, DE, flash, Hit, Jump, LO, PB, Sliding, Thunder, Typing = 14 } */

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();

        BGMClips = new AudioClip[4];
        BGMClips[0] = Resources.Load<AudioClip>("메인타이틀");
        BGMClips[1] = Resources.Load<AudioClip>("챕터1");
        BGMClips[2] = Resources.Load<AudioClip>("챕터2");
        BGMClips[3] = Resources.Load<AudioClip>("챕터3");
    }

    private void Update()
    {
        BGMPlayer.volume = BGMVoulme; 

    }

    public void PlayBGM(int Index)
    {
        if (Index < 0 || Index >= BGMClips.Length)
        {
            Debug.LogError("BGM 인덱스가 범위를 벗어났습니다: " + Index);
            return;
        }
        if (BGMPlayer.clip == BGMClips[Index] && BGMPlayer.isPlaying)
        {
            return;
        }
      
        Debug.Log("BGM 재생: " + BGMClips[Index].name);
        BGMPlayer.clip = BGMClips[Index];
        BGMPlayer.Play();
    }

    public void StopBGM()
    {
        if (BGMPlayer.isPlaying)
        {
            BGMPlayer.Stop();
        }
    }

    public void PauseBGM()
    {
        if (BGMPlayer != null && BGMPlayer.isPlaying)
        {
            BGMPlayer.Pause(); // 일시정지
        }
    }

    public void UnPauseBGM()
    {
        if (BGMPlayer != null)
        {
            BGMPlayer.UnPause(); // 이어서 재생
        }
    }
    

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject BGMObject = new GameObject("BGMPlayer");
        Debug.Log("BGMPlayer 오브젝트가 생성되었습니다");
        BGMObject.transform.parent = transform;
        BGMPlayer = BGMObject.AddComponent<AudioSource>();
        BGMPlayer.outputAudioMixerGroup = audioMixer;
        BGMPlayer.loop = true;
        BGMPlayer.volume = BGMVoulme;
        BGMPlayer.playOnAwake = false;
        BGMPlayer.outputAudioMixerGroup = BGMMixerGroup;

        // 효과음 플레이어 초기화
       /* GameObject SFXObject = new GameObject("SFXPlayer");
        Debug.Log("SFXPlayer 오브젝트가 생성되었습니다");
        SFXObject.transform.parent = transform;
        SFXPlayers = new AudioSource[channels];
        for (int index = 0; index < SFXPlayers.Length; index++)
        {
            SFXPlayers[index] = SFXObject.AddComponent<AudioSource>();
            SFXPlayers[index].outputAudioMixerGroup = audioMixer; // <-- 이 한 줄만 추가
            SFXPlayers[index].playOnAwake = false;
            SFXPlayers[index].volume = SFXVolume;
        }
    }

    public void PlaySFX(SFX SFX)
    {
        for (int index = 0; index < SFXPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % SFXPlayers.Length;

            if (SFXPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            SFXPlayers[loopIndex].clip = SFXClips[(int)SFX];
            SFXPlayers[loopIndex].Play();
            break;


        } */

    }
    public IEnumerator MusicFadein()
    {
        float duration = 0.5f;
        float time = 0f;
        float start = 0.5f;
        float end = 0f;

        while (time < duration)
        {
            BGMVoulme = Mathf.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator MusicFadeout()
    {
        float duration = 0.5f;
        float time = 0f;
        float start = 0f;
        float end = 0.5f;

        while (time < duration)
        {
            BGMVoulme = Mathf.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
