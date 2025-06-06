using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;   

public class ButtonManager : MonoBehaviour
{
    //[SerializeField] private string nextSceneName = "ImageBoard"; // 유연하게 다음 씬 이름 설정
    [SerializeField] private Button startButton; // 시작 버튼 연결
    [SerializeField] AudioManager audioManager; // 오디오 매니저 연결

    public AudioClip MainMusic; // 메인 음악 클립
    public AudioClip Chapter1; // 챕터 1 음악 클립
    public AudioClip Chapter2; // 챕터 2 음악 클립
    public AudioClip Chapter3; // 챕터 3 음악 클립
    

    private void Start()
    {
        if (startButton == null)
        {
            startButton = GetComponent<Button>(); // 자동 할당 (혹시 인스펙터에서 안 연결했을 경우 대비)
        }
        startButton.onClick.AddListener(OnStartButtonClick); // 버튼 클릭 이벤트 연결

        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>(); // AudioManager 찾기
        }

        if (audioManager != null)
        {
            Debug.Log("AudioManager found: " + audioManager.name);
            AudioManager.instance.PlayBGM(0); // BGM 재생 (메인 타이틀 음악)
        }
    }
    public void OnStartButtonClick()
    {
        SoundManager.Instance.Play("PB");
        Debug.Log("Start button clicked");
        LoadNextScene(); // 씬 전환
    }

    public void LoadNextScene()
    {
        if (audioManager != null)
        {
            AudioManager.instance.PauseBGM();
        }
        SceneManager.LoadScene("ImageBoard"); // 변수로 설정된 씬 로드
    }
}
