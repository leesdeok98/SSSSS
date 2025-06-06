using System.Collections;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    [Header("Chapter")]
    [SerializeField] private SpriteRenderer chapterRenderer;
    [SerializeField] private Sprite[] chapterImages;

    [SerializeField] private float fadeDuration = 1f;

    private int currentChapterIndex = 0;

    private void Start()
    {
        // �ʱ� é�� ����
        chapterRenderer.sprite = chapterImages[0];
        SetAlpha(chapterRenderer, 0f);
        chapterRenderer.gameObject.SetActive(true);

        // é�� 1 ���̵���
        StartCoroutine(Fade(chapterRenderer, 0f, 1f, fadeDuration));
    }

    public void TryChangeChapter()
    {
        if (currentChapterIndex < chapterImages.Length - 1)
        {
            int nextIndex = currentChapterIndex + 1;
            StartCoroutine(ChangeChapter(nextIndex));
            currentChapterIndex = nextIndex;
        }
    }

    private IEnumerator ChangeChapter(int nextIndex)
    {
        // ���� é�� ���̵�ƿ�
        yield return StartCoroutine(Fade(chapterRenderer, 1f, 0f, fadeDuration));

        // ���� �̹����� ��ü
        chapterRenderer.sprite = chapterImages[nextIndex];

        // ���� é�� ���̵���
        yield return StartCoroutine(Fade(chapterRenderer, 0f, 1f, fadeDuration));
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

    private void SetAlpha(SpriteRenderer renderer, float alpha)
    {
        Color color = renderer.color;
        color.a = alpha;
        renderer.color = color;
    }
}
