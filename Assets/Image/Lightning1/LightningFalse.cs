using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFalse : MonoBehaviour
{
    public float fadeDuration = 1.0f; // ���������� �ð�(��)
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        // ������Ʈ�� ���� �� ���İ��� 1�� ����
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

        gameObject.SetActive(false); // ��Ȱ��ȭ
    }
}
