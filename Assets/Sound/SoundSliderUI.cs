using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSliderUI : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider BGMSlider;
    public Slider SFXSlider;
    public Slider MASTERSlider;


    public void Start()
    {
        MASTERSlider.value = 0.50005f;
        BGMSlider.value = 0.50005f;
        SFXSlider.value = 0.50005f;
        audioMixer.SetFloat("BGM", Mathf.Log10(BGMSlider.value) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(SFXSlider.value) * 20);
        audioMixer.SetFloat("MASTER", Mathf.Log10(MASTERSlider.value) * 20);
    }

    public void BGMSetLevel(float value)
    {
        if (value <= 0.001f) // 거의 0에 가까우면 완전 음소거
        {
            audioMixer.SetFloat("BGM", -80f);
        }
        else
        {
            audioMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
        }
    }
    public void SFXSetLevel(float value)
    {
        if (value <= 0.001f) 
        {
            audioMixer.SetFloat("MASTER", -80f); 
        }
        else
        {
            audioMixer.SetFloat("MASTER", Mathf.Log10(value) * 20);
        }
    }
    public void MASTERSetLevel(float value)
    {
        if (value <= 0.001f) 
        {
            audioMixer.SetFloat("SFX", -80f);
        }
        else
        {
            audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
        }
    }
}