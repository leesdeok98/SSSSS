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
            Button = GetComponent<Button>(); // �ڵ� �Ҵ� (Ȥ�� �ν����Ϳ��� �� �������� ��� ���)
        }
        Button.onClick.AddListener(OnStartButtonClick); // ��ư Ŭ�� �̺�Ʈ ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnStartButtonClick()
    {
        Debug.Log("Start button clicked");
        SoundManager.Instance.Play("PB");
        LoadNextScene(); // �� ��ȯ
    }
    public void LoadNextScene()
    { 
        SceneManager.LoadScene("GameView"); // ������ ������ �� �ε�
    }
}
