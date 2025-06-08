using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearFadeEffect : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;     // 이부분이 페이드 시간

    private void Awake()
    {
        canvasGroup.alpha = 0f;
    }

    public IEnumerator StartFadeOut()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }
}
