using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider masterVolumeSlider;  // ������ ���� �����̴�
    public Slider bgmVolumeSlider;     // BGM ���� �����̴�
    public Slider sfxVolumeSlider;     // SFX ���� �����̴�

    private const string MasterVolumePrefKey = "MasterVolume";
    private const string BgmVolumePrefKey = "BgmVolume";
    private const string SfxVolumePrefKey = "SfxVolume";

    public static object Instance { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs���� ����� ���� ���� �ҷ����� (�⺻���� 0.5f)
        float savedMasterVolume = PlayerPrefs.GetFloat(MasterVolumePrefKey, 0.5f);
        float savedBgmVolume = PlayerPrefs.GetFloat(BgmVolumePrefKey, 0.5f);
        float savedSfxVolume = PlayerPrefs.GetFloat(SfxVolumePrefKey, 0.5f);

        // �����̴��� �ʱ� ���� �ҷ��� ���� ������ ����
        masterVolumeSlider.value = savedMasterVolume;
        bgmVolumeSlider.value = savedBgmVolume;
        sfxVolumeSlider.value = savedSfxVolume;

        // �� �����̴� ���� ����� �� ȣ��� �̺�Ʈ ����
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        bgmVolumeSlider.onValueChanged.AddListener(OnBgmVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeChanged);

        // �ʱ� ������ ���� ����
        SetVolume(savedMasterVolume, savedBgmVolume, savedSfxVolume);
    }

    // ������ ���� ���� ����Ǿ��� �� ȣ��Ǵ� �޼ҵ�
    public void OnMasterVolumeChanged(float volume)
    {
        // ������ ������ ����
        SetVolume(volume, bgmVolumeSlider.value, sfxVolumeSlider.value);

        // ����� ���� ���� PlayerPrefs�� ����
        PlayerPrefs.SetFloat(MasterVolumePrefKey, volume);
        PlayerPrefs.Save();
    }

    // BGM ���� ���� ����Ǿ��� �� ȣ��Ǵ� �޼ҵ�
    public void OnBgmVolumeChanged(float volume)
    {
        // BGM ������ ����
        SetVolume(masterVolumeSlider.value, volume, sfxVolumeSlider.value);

        // ����� ���� ���� PlayerPrefs�� ����
        PlayerPrefs.SetFloat(BgmVolumePrefKey, volume);
        PlayerPrefs.Save();
    }

    // SFX ���� ���� ����Ǿ��� �� ȣ��Ǵ� �޼ҵ�
    public void OnSfxVolumeChanged(float volume)
    {
        // SFX ������ ����
        SetVolume(masterVolumeSlider.value, bgmVolumeSlider.value, volume);

        // ����� ���� ���� PlayerPrefs�� ����
        PlayerPrefs.SetFloat(SfxVolumePrefKey, volume);
        PlayerPrefs.Save();
    }

    // �� ���� ������ �����ϴ� �޼ҵ�
    private void SetVolume(float masterVolume, float bgmVolume, float sfxVolume)
    {
        // ������ ���� (AudioListener)
        AudioListener.volume = masterVolume;

        // BGM ���� (BGM ����� �ҽ�)
        AudioSource bgmSource = GameObject.Find("BGMSource").GetComponent<AudioSource>();
        if (bgmSource != null)
            bgmSource.volume = bgmVolume;

        // SFX ���� (SFX ����� �ҽ�)
        AudioSource sfxSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();
        if (sfxSource != null)
            sfxSource.volume = sfxVolume;
    }

    public static implicit operator SoundManager(AudioManager v)
    {
        throw new NotImplementedException();
    }
}
