using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Nomal : MonoBehaviour
{
    [SerializeField] private Button Button;
    // Start is called before the first frame update
    void Start()
    {
        if (Button == null)
        {
            Button = GetComponent<Button>(); // 자동 할당 (혹시 인스펙터에서 안 연결했을 경우 대비)
        }
        Button.onClick.AddListener(OnStartButtonClick); // 버튼 클릭 이벤트 연결
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnStartButtonClick()
    {
        Debug.Log("Start button clicked");
        LoadNextScene(); // 씬 전환
    }
    public void LoadNextScene()
    {
        //if (audioManager != null)
        //{
        //    audioManager.PlayBGM(0);
        //}
        SceneManager.LoadScene("GameView"); // 변수로 설정된 씬 로드
    }
}
