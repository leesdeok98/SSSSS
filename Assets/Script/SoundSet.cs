using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SoundSet : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject Sound;
    [SerializeField] private GameObject Xkey;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // 게임 시작 시 일시정지창 숨기기
        pausePanel.SetActive(false);
        Sound.SetActive(false);
        Xkey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            if (isPaused)
                return;
        }
    }
    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // 게임 일시정지
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // 게임 재개
            pausePanel.SetActive(false);
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
        Debug.Log("QuitToMain 함수 실행됨");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }
    public void OnClickSound()
    {
        Debug.Log("사운드");
        if (isPaused)
        Sound.SetActive(true);


    }
    public void OnClickXkey()
    {
        Sound.SetActive(false);

    }
    
}
