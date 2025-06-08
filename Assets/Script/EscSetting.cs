using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SoundSet : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private AudioManager audioManager;



    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
        pausePanel.SetActive(false);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameOverPanel != null && gameOverPanel.activeSelf)
                return;

            TogglePause();
        }
    }
    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // ���� �Ͻ�����
            pausePanel.SetActive(true);
            audioManager.PauseBGM(); // BGM 정지
            Debug.Log("BGM 멈춤");
        }
        else
        {
            Time.timeScale = 1f; // ���� �簳
            pausePanel.SetActive(false);
            audioManager.UnPauseBGM(); // BGM 다시 재생
            Debug.Log("BGM 시작");
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void QuitToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }
    
    public void OnClickRetry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameView");
    }

}