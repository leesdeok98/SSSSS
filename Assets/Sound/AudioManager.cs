using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public AudioClip[] BGMClips;
    public float BGMVoulme;
    AudioSource BGMPlayer;


    [Header("#SFX)")]
    public AudioClip[] SFXClips;
    public float SFXVolume;
    public int channels;
    AudioSource[] SFXPlayers;
    int channelIndex;

    /* public enum SFX { SFX �̸� } */

    void Awake()
    {
        instance = this;
        Init();

        BGMClips = new AudioClip[4];
        BGMClips[0] = Resources.Load<AudioClip>("����Ÿ��Ʋ");
        BGMClips[1] = Resources.Load<AudioClip>("é��1");
        BGMClips[2] = Resources.Load<AudioClip>("é��2");
        BGMClips[3] = Resources.Load<AudioClip>("é��3");
    }

    public void PlayBGM(int Index)
    {
        if (Index < 0 || Index >= BGMClips.Length)
        {
            Debug.LogError("BGM �ε����� ������ ������ϴ�: " + Index);
            return;
        }
        if (BGMPlayer.clip == BGMClips[Index] && BGMPlayer.isPlaying)
        {
            return;
        }

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
        if (BGMPlayer != null &&  BGMPlayer.isPlaying)
        {
            BGMPlayer.Pause(); // �Ͻ�����
        }
    }

    public void UnPauseBGM()
    {
        if (BGMPlayer != null)
        {
            BGMPlayer.UnPause(); // �̾ ���
        }    
    }
    void Start()
    {
        PlayBGM(0); // �ʱ� BGM ���, �ε��� 0�� ���� Ÿ��Ʋ BGM 
    }

    void Init()
    {
        // ����� �÷��̾� �ʱ�ȭ
        GameObject BGMObject = new GameObject("BGMPlayer");
        Debug.Log("BGMPlayer ������Ʈ�� �����Ǿ����ϴ�");
        BGMObject.transform.parent = transform;
        BGMPlayer = BGMObject.AddComponent<AudioSource>();
        BGMPlayer.outputAudioMixerGroup = audioMixer;
        BGMPlayer.loop = true;
        BGMPlayer.volume = BGMVoulme;
        BGMPlayer.playOnAwake = false;
        BGMPlayer.outputAudioMixerGroup = BGMMixerGroup;

        // ȿ���� �÷��̾� �ʱ�ȭ
        GameObject SFXObject = new GameObject("SFXPlayer");
        Debug.Log("SFXPlayer ������Ʈ�� �����Ǿ����ϴ�");
        SFXObject.transform.parent = transform;
        SFXPlayers = new AudioSource[channels];
        for (int index = 0; index < SFXPlayers.Length; index++)
        {
            SFXPlayers[index] = SFXObject.AddComponent<AudioSource>();
            SFXPlayers[index].outputAudioMixerGroup = audioMixer; // <-- �� �� �ٸ� �߰�
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
