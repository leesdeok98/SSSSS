using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;   

public class ButtonManager : MonoBehaviour
{
    //[SerializeField] private string nextSceneName = "ImageBoard"; // �����ϰ� ���� �� �̸� ����
    [SerializeField] private Button startButton; // ���� ��ư ����
    [SerializeField] AudioManager audioManager; // ����� �Ŵ��� ����

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

        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>(); // AudioManager ã��
        }

        if (audioManager != null)
        {
            Debug.Log("AudioManager found: " + audioManager.name);
            AudioManager.instance.PlayBGM(0); // BGM ��� (���� Ÿ��Ʋ ����)
        }
    }
    public void OnStartButtonClick()
    {
        SoundManager.Instance.Play("PB");
        Debug.Log("Start button clicked");
        LoadNextScene(); // �� ��ȯ
    }

    public void LoadNextScene()
    {
        if (audioManager != null)
        {
            AudioManager.instance.PauseBGM();
        }
        SceneManager.LoadScene("ImageBoard"); // ������ ������ �� �ε�
    }
}
