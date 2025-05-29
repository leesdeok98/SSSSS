using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioMixerGroup audioMixer;
    [SerializeField] AudioMixerGroup BGMMixerGroup;
    [SerializeField] AudioMixerGroup SFXMixerGroup;

    [Header("#BGM)")]
    public AudioClip BGMClip;
    public float BGMVoulme;
    AudioSource BGMPlayer;

    [Header("#SFX)")]
    public AudioClip[] SFXClips;
    public float SFXVolume;
    public int channels;
    AudioSource[] SFXPlayers;
    int channelIndex;

    /* public enum SFX { SFX 이름 } */

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject BGMObject = new GameObject("BGMPlayer");
        Debug.Log("BGMPlayer 오브젝트가 생성되었습니다");
        BGMObject.transform.parent = transform;
        BGMPlayer = BGMObject.AddComponent<AudioSource>();
        BGMPlayer.clip = BGMClip;
        BGMPlayer.outputAudioMixerGroup = audioMixer;
        BGMPlayer.loop = true;
        BGMPlayer.volume = BGMVoulme;
        BGMPlayer.playOnAwake = false;
        BGMPlayer.Play();
        BGMPlayer.outputAudioMixerGroup = BGMMixerGroup;

        // 효과음 플레이어 초기화
        GameObject SFXObject = new GameObject("SFXPlayer");
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

    /*public void PlaySFX(SFX SFX)
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


        }*/
}
