using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSliderUI : MonoBehaviour
{
    public AudioMixer audioMixer;

    public AudioSource audioSource;
  

    public Slider MASTERSlider;
    public Slider BGMSlider;
    public Slider SFXSlider;


    public void Start()
    {
        MASTERSlider.value = 0.50005f;
        BGMSlider.value = 0.50005f;
        SFXSlider.value = 0.50005f;
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(BGMSlider.value) * 20);
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(SFXSlider.value) * 20);
        audioMixer.SetFloat("MASTERVolume", Mathf.Log10(MASTERSlider.value) * 20);
    }
    public void BGMSetLevel(float value)
    {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(value) * 20);
    }
    public void SFXSetLevel(float value)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
    }
    public void MASTERSetLevel(float value)
    {
        audioMixer.SetFloat("MASTERVolume", Mathf.Log10(value) * 20);
    }
}