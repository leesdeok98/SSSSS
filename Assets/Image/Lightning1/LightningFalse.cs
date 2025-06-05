using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFalse : MonoBehaviour
{
    public float fadeDuration = 1.0f; // 투명해지는 시간(초)
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        // 오브젝트가 켜질 때 알파값을 1로 복원
        Color color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;

        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsed = 0f;
        Color color = spriteRenderer.color;
        float startAlpha = color.a;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
            spriteRenderer.color = color;
            yield return null;
        }

        color.a = 0f;
        spriteRenderer.color = color;

        gameObject.SetActive(false); // 비활성화
    }
}
