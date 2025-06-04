using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeEffect : MonoBehaviour
{
    public static FadeEffect Instance;

    [SerializeField] private float fadeInTime = 0.5f;
    [SerializeField] private float fadeOutTime = 1.5f;

    private Image image;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        image = GetComponent<Image>();
    }

    public IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = image.color;
        color.a = startAlpha;
        image.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            color.a = newAlpha;
            image.color = color;
            yield return null;
        }

        color.a = endAlpha;
        image.color = color;
    }

    public IEnumerator FadeIn()
    {
        yield return StartCoroutine(Fade(1f, 0f, fadeInTime));
    }

    public IEnumerator FadeOut()
    {
        yield return StartCoroutine(Fade(0f, 1f, fadeOutTime));
    }
}