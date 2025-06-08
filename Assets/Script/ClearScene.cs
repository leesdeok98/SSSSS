using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScene : MonoBehaviour
{
    [Header("¾À½Ã¹ß")]
    public SpriteRenderer ClearRenderer;
    public float fadeDuration;

    public void Start()
    {
        StartCoroutine(clearFadeIn());
    }

    public IEnumerator clearFadeIn()
    {
        SetAlpha(ClearRenderer, 0f);
        ClearRenderer.gameObject.SetActive(true);

        yield return StartCoroutine(Fade(ClearRenderer, 0f, 1f, fadeDuration));
    }
   
    
    private void SetAlpha(SpriteRenderer renderer, float alpha)
    {
        Color color = renderer.color;
        color.a = alpha;
        renderer.color = color;
    }
    private IEnumerator Fade(SpriteRenderer renderer, float fromAlpha, float toAlpha, float duration)    
    {
        float time = 0f;
        Color color = renderer.color;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, time / duration);
            color.a = alpha;
            renderer.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        color.a = toAlpha;
        renderer.color = color;
    }

}
