using UnityEngine;
using System.Collections;

public class MainBGM : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(PlayBGMDelayed());
    }

    IEnumerator PlayBGMDelayed()
    {
        yield return null; // �� ������ ���

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayBGM(0);
        }
        else
        {
            Debug.LogError("AudioManager.instance is null!");
        }
    }
}