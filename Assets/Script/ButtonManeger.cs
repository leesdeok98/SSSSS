using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    //[SerializeField] private string nextSceneName = "ImageBoard"; // 유연하게 다음 씬 이름 설정
    [SerializeField] private Button startButton; // 시작 버튼 연결

    private void Start()
    {
        if (startButton == null)
        {
            startButton = GetComponent<Button>(); // 자동 할당 (혹시 인스펙터에서 안 연결했을 경우 대비)
        }
        startButton.onClick.AddListener(OnStartButtonClick); // 버튼 클릭 이벤트 연결
    }

    public void OnStartButtonClick()
    {
        Debug.Log("Start button clicked");
        LoadNextScene(); // 씬 전환
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("ImageBoard"); // 변수로 설정된 씬 로드
    }
}
