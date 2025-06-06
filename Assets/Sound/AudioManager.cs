using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class BGMClipData
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 0.5f;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] AudioMixerGroup audioMixer;
    [SerializeField] AudioMixerGroup BGMMixerGroup;
    [SerializeField] AudioMixerGroup SFXMixerGroup;

    [Header("BGM Settings")]
    public BGMClipData[] BGMClips;

    [Header("Master BGM Volume")]
    [Range(0f, 1f)]
    public float masterBGMVolume = 1f;

    AudioSource BGMPlayer;
    private int currentBGMIndex = -1;

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
            return;
        }

        Init();
        LoadBGMClips();
    }

    private void Update()
    {
        if (currentBGMIndex >= 0 && currentBGMIndex < BGMClips.Length)
        {
            BGMPlayer.volume = BGMClips[currentBGMIndex].volume * masterBGMVolume;
        }
    }

    void LoadBGMClips()
    {
        if (BGMClips == null || BGMClips.Length == 0)
        {
            BGMClips = new BGMClipData[4];

            BGMClips[0] = new BGMClipData();
            BGMClips[0].name = "메인 타이틀";
            BGMClips[0].clip = Resources.Load<AudioClip>("메인타이틀");
            BGMClips[0].volume = 0.5f;

            BGMClips[1] = new BGMClipData();
            BGMClips[1].name = "챕터 1";
            BGMClips[1].clip = Resources.Load<AudioClip>("챕터1");
            BGMClips[1].volume = 0.5f;

            BGMClips[2] = new BGMClipData();
            BGMClips[2].name = "챕터 2";
            BGMClips[2].clip = Resources.Load<AudioClip>("챕터2");
            BGMClips[2].volume = 0.5f;

            BGMClips[3] = new BGMClipData();
            BGMClips[3].name = "챕터 3";
            BGMClips[3].clip = Resources.Load<AudioClip>("챕터3");
            BGMClips[3].volume = 0.5f;
        }
    }

    public void PlayBGM(int Index)
    {
        if (Index < 0 || Index >= BGMClips.Length)
        {
            Debug.LogError("BGM 인덱스가 범위를 벗어났습니다: " + Index);
            return;
        }

        if (BGMClips[Index].clip == null)
        {
            Debug.LogError($"BGM 클립이 null입니다. 인덱스: {Index}");
            return;
        }

        if (BGMPlayer.clip == BGMClips[Index].clip && BGMPlayer.isPlaying)
        {
            return;
        }

        currentBGMIndex = Index;
        Debug.Log($"BGM 재생: {BGMClips[Index].name} (볼륨: {BGMClips[Index].volume})");

        BGMPlayer.clip = BGMClips[Index].clip;
        BGMPlayer.volume = BGMClips[Index].volume * masterBGMVolume;
        BGMPlayer.Play();
    }

    public void StopBGM()
    {
        if (BGMPlayer.isPlaying)
        {
            BGMPlayer.Stop();
            currentBGMIndex = -1;
        }
    }

    public void PauseBGM()
    {
        if (BGMPlayer != null && BGMPlayer.isPlaying)
        {
            BGMPlayer.Pause();
        }
    }

    public void UnPauseBGM()
    {
        if (BGMPlayer != null)
        {
            BGMPlayer.UnPause();
        }
    }

    public void SetBGMVolume(int index, float volume)
    {
        if (index >= 0 && index < BGMClips.Length)
        {
            BGMClips[index].volume = Mathf.Clamp01(volume);

            if (currentBGMIndex == index)
            {
                BGMPlayer.volume = BGMClips[index].volume * masterBGMVolume;
            }
        }
    }

    public void SetMasterBGMVolume(float volume)
    {
        masterBGMVolume = Mathf.Clamp01(volume);

        if (currentBGMIndex >= 0 && currentBGMIndex < BGMClips.Length)
        {
            BGMPlayer.volume = BGMClips[currentBGMIndex].volume * masterBGMVolume;
        }
    }

    public void ResetAllBGMVolumes()
    {
        for (int i = 0; i < BGMClips.Length; i++)
        {
            BGMClips[i].volume = 0.5f;
        }
        masterBGMVolume = 1f;
    }

    void Init()
    {
        GameObject BGMObject = new GameObject("BGMPlayer");
        Debug.Log("BGMPlayer 오브젝트가 생성되었습니다");
        BGMObject.transform.parent = transform;
        BGMPlayer = BGMObject.AddComponent<AudioSource>();
        BGMPlayer.outputAudioMixerGroup = BGMMixerGroup;
        BGMPlayer.loop = true;
        BGMPlayer.playOnAwake = false;
    }

    public IEnumerator MusicFadein(float targetVolume = -1f)
    {
        if (currentBGMIndex < 0) yield break;

        float target = targetVolume >= 0 ? targetVolume : BGMClips[currentBGMIndex].volume;
        float duration = 0.5f;
        float time = 0f;
        float start = 0f;

        while (time < duration)
        {
            float currentVolume = Mathf.Lerp(start, target, time / duration);
            BGMPlayer.volume = currentVolume * masterBGMVolume;
            time += Time.deltaTime;
            yield return null;
        }

        BGMPlayer.volume = target * masterBGMVolume;
    }

    public IEnumerator MusicFadeout()
    {
        if (currentBGMIndex < 0) yield break;

        float duration = 0.5f;
        float time = 0f;
        float start = BGMPlayer.volume;
        float end = 0f;

        while (time < duration)
        {
            BGMPlayer.volume = Mathf.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        BGMPlayer.volume = 0f;
    }
}