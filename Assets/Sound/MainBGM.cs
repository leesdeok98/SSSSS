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
        yield return null; // 한 프레임 대기

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