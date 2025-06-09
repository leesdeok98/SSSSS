using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundSetting : MonoBehaviour
{

    public AudioSource titleMusic;
    public AudioSource c1;
    public AudioSource c2;
    public AudioSource c3;
    public AudioSource clear;
    public AudioSource PB;
    public AudioSource BA;
    public AudioSource BAp;
    public AudioSource BD;
    public AudioSource BL;
    public AudioSource DE;
    public AudioSource Dead;
    public AudioSource flash;
    public AudioSource Hit;
    public AudioSource Jump;
    public AudioSource LO;
    public AudioSource Sliding;
    public AudioSource Thunder1;
    public AudioSource Thunder2;
    public AudioSource Typing;

    void Start()
    {
        float titlebgm = PlayerPrefs.GetFloat("titleBGM", 0.2f);
        float bgm = PlayerPrefs.GetFloat("BGM", 0.5f);
        float PBsfx = PlayerPrefs.GetFloat("PBSFX", 0.5f);
        float BAsfx = PlayerPrefs.GetFloat("BASFX", 0.2f);
        float BApsfx = PlayerPrefs.GetFloat("BApSFX", 0.2f);
        float BDsfx = PlayerPrefs.GetFloat("BDSFX", 0.2f);
        float BLsfx = PlayerPrefs.GetFloat("BLSFX", 0.2f);
        float DEsfx = PlayerPrefs.GetFloat("DESFX", 0.3f);
        float Deadsfx = PlayerPrefs.GetFloat("DeadSFX", 1f);
        float flashsfx = PlayerPrefs.GetFloat("flashSFX", 0.5f);
        float Hitsfx = PlayerPrefs.GetFloat("HitSFX", 1f);
        float Jumpsfx = PlayerPrefs.GetFloat("JumpSFX", 2f);
        float LOsfx = PlayerPrefs.GetFloat("LOSFX", 0.5f);
        float Slidingsfx = PlayerPrefs.GetFloat("SlidingSFX", 0.5f);
        float Thunder1sfx = PlayerPrefs.GetFloat("Thunder1SFX", 0.3f);
        float Thunder2sfx = PlayerPrefs.GetFloat("Thunder2SFX", 0.3f);
        float Typingsfx = PlayerPrefs.GetFloat("TypingSFX", 0.5f);

        titleMusic.volume = titlebgm;
        c1.volume = bgm;
        c2.volume = bgm;
        c3.volume = bgm;
        clear.volume = bgm;

        PB.volume = PBsfx;
        BA.volume = BAsfx;
        BAp.volume = BApsfx;
        BD.volume = BDsfx;
        BL.volume = BLsfx;
        DE.volume = DEsfx;
        Dead.volume = Deadsfx;
        flash.volume = flashsfx;
        Hit.volume = Hitsfx;
        Jump.volume = Jumpsfx;
        LO.volume = LOsfx;
        Sliding.volume = Slidingsfx;
        Thunder1.volume = Thunder1sfx;
        Thunder2.volume = Thunder2sfx;
        Typing.volume = Typingsfx;

    }

    public void SetMusicVolume(float volume)
    {
        titleMusic.volume = volume;
        c1.volume = volume;
        c2.volume = volume;
        c3.volume = volume;
        clear.volume = volume;
        PlayerPrefs.SetFloat("titleBGM", volume);
        PlayerPrefs.SetFloat("BGM", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        PB.volume = volume;
        BA.volume = volume;
        BAp.volume = volume;
        BD.volume = volume;
        BL.volume = volume;
        DE.volume = volume;
        Dead.volume = volume;
        flash.volume = volume;
        Hit.volume = volume;
        Jump.volume = volume;
        LO.volume = volume;
        Sliding.volume = volume;
        Thunder1.volume = volume;
        Thunder2.volume = volume;
        Typing.volume = volume;

        PlayerPrefs.SetFloat("PBSFX", volume);
        PlayerPrefs.SetFloat("BASFX", volume);
        PlayerPrefs.SetFloat("BApSFX", volume);
        PlayerPrefs.SetFloat("BDSFX", volume);
        PlayerPrefs.SetFloat("BLSFX", volume);
        PlayerPrefs.SetFloat("DESFX", volume);
        PlayerPrefs.SetFloat("DeadSFX", volume);
        PlayerPrefs.SetFloat("flashSFX", volume);
        PlayerPrefs.SetFloat("HitSFX", volume);
        PlayerPrefs.SetFloat("JumpSFX", volume);
        PlayerPrefs.SetFloat("LOSFX", volume);
        PlayerPrefs.SetFloat("SlidingSFX", volume);
        PlayerPrefs.SetFloat("Thunder1SFX", volume);
        PlayerPrefs.SetFloat("Thunder2SFX", volume);
        PlayerPrefs.SetFloat("TypingSFX", volume);
        PlayerPrefs.Save();


    }
}