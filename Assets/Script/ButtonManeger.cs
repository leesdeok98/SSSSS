using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    //[SerializeField] private string nextSceneName = "ImageBoard"; // �����ϰ� ���� �� �̸� ����
    [SerializeField] private Button startButton; // ���� ��ư ����

    public AudioClip MainMusic; // ���� ���� Ŭ��
    public AudioClip Chapter1; // é�� 1 ���� Ŭ��
    public AudioClip Chapter2; // é�� 2 ���� Ŭ��
    public AudioClip Chapter3; // é�� 3 ���� Ŭ��
    

    private void Start()
    {
        if (startButton == null)
        {
            startButton = GetComponent<Button>(); // �ڵ� �Ҵ� (Ȥ�� �ν����Ϳ��� �� �������� ��� ���)
        }
        startButton.onClick.AddListener(OnStartButtonClick); // ��ư Ŭ�� �̺�Ʈ ����

        AudioManager.instance.PlayBGM(0); // ���� Ÿ��Ʋ ���� ���
    }
    public void OnStartButtonClick()
    {
        Debug.Log("Start button clicked");
        LoadNextScene(); // �� ��ȯ
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene("ImageBoard"); // ������ ������ �� �ε�
        AudioManager.instance.PlayBGM(0);
    }
}
