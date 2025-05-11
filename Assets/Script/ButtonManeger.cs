using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Main2";   // 다음에 로드할 씬
    [SerializeField] private Button startButton;         // 시작 버튼 연결


    private void Start()
    {
        if (startButton == null)
        {
            startButton = GetComponent<Button>();
        }
        startButton.onClick.AddListener(OnStartButtonClick);
    }
    public void OnStartButtonClick()
    {
        Debug.Log("Start button clicked");
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}