using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    //[SerializeField] private string nextSceneName = "ImageBoard"; // �����ϰ� ���� �� �̸� ����
    [SerializeField] private Button startButton; // ���� ��ư ����

    private void Start()
    {
        if (startButton == null)
        {
            startButton = GetComponent<Button>(); // �ڵ� �Ҵ� (Ȥ�� �ν����Ϳ��� �� �������� ��� ���)
        }
        startButton.onClick.AddListener(OnStartButtonClick); // ��ư Ŭ�� �̺�Ʈ ����
    }

    public void OnStartButtonClick()
    {
        Debug.Log("Start button clicked");
        LoadNextScene(); // �� ��ȯ
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("ImageBoard"); // ������ ������ �� �ε�
    }
}
