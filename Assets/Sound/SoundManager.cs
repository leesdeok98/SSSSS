using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider masterVolumeSlider;  // 마스터 볼륨 슬라이더
    public Slider bgmVolumeSlider;     // BGM 볼륨 슬라이더
    public Slider sfxVolumeSlider;     // SFX 볼륨 슬라이더

    private const string MasterVolumePrefKey = "MasterVolume";
    private const string BgmVolumePrefKey = "BgmVolume";
    private const string SfxVolumePrefKey = "SfxVolume";

    public static object Instance { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs에서 저장된 볼륨 값을 불러오기 (기본값은 0.5f)
        float savedMasterVolume = PlayerPrefs.GetFloat(MasterVolumePrefKey, 0.5f);
        float savedBgmVolume = PlayerPrefs.GetFloat(BgmVolumePrefKey, 0.5f);
        float savedSfxVolume = PlayerPrefs.GetFloat(SfxVolumePrefKey, 0.5f);

        // 슬라이더의 초기 값을 불러온 볼륨 값으로 설정
        masterVolumeSlider.value = savedMasterVolume;
        bgmVolumeSlider.value = savedBgmVolume;
        sfxVolumeSlider.value = savedSfxVolume;

        // 각 슬라이더 값이 변경될 때 호출될 이벤트 설정
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        bgmVolumeSlider.onValueChanged.AddListener(OnBgmVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeChanged);

        // 초기 값으로 볼륨 적용
        SetVolume(savedMasterVolume, savedBgmVolume, savedSfxVolume);
    }

    // 마스터 볼륨 값이 변경되었을 때 호출되는 메소드
    public void OnMasterVolumeChanged(float volume)
    {
        // 마스터 볼륨을 설정
        SetVolume(volume, bgmVolumeSlider.value, sfxVolumeSlider.value);

        // 변경된 볼륨 값을 PlayerPrefs에 저장
        PlayerPrefs.SetFloat(MasterVolumePrefKey, volume);
        PlayerPrefs.Save();
    }

    // BGM 볼륨 값이 변경되었을 때 호출되는 메소드
    public void OnBgmVolumeChanged(float volume)
    {
        // BGM 볼륨을 설정
        SetVolume(masterVolumeSlider.value, volume, sfxVolumeSlider.value);

        // 변경된 볼륨 값을 PlayerPrefs에 저장
        PlayerPrefs.SetFloat(BgmVolumePrefKey, volume);
        PlayerPrefs.Save();
    }

    // SFX 볼륨 값이 변경되었을 때 호출되는 메소드
    public void OnSfxVolumeChanged(float volume)
    {
        // SFX 볼륨을 설정
        SetVolume(masterVolumeSlider.value, bgmVolumeSlider.value, volume);

        // 변경된 볼륨 값을 PlayerPrefs에 저장
        PlayerPrefs.SetFloat(SfxVolumePrefKey, volume);
        PlayerPrefs.Save();
    }

    // 세 가지 볼륨을 적용하는 메소드
    private void SetVolume(float masterVolume, float bgmVolume, float sfxVolume)
    {
        // 마스터 볼륨 (AudioListener)
        AudioListener.volume = masterVolume;

        // BGM 볼륨 (BGM 오디오 소스)
        AudioSource bgmSource = GameObject.Find("BGMSource").GetComponent<AudioSource>();
        if (bgmSource != null)
            bgmSource.volume = bgmVolume;

        // SFX 볼륨 (SFX 오디오 소스)
        AudioSource sfxSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();
        if (sfxSource != null)
            sfxSource.volume = sfxVolume;
    }

    public static implicit operator SoundManager(AudioManager v)
    {
        throw new NotImplementedException();
    }
}
