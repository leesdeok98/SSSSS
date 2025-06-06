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
        // 초기 챕터 설정
        chapterRenderer.sprite = chapterImages[0];
        SetAlpha(chapterRenderer, 0f);
        chapterRenderer.gameObject.SetActive(true);

        // 챕터 1 페이드인
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
        // 현재 챕터 페이드아웃
        yield return StartCoroutine(Fade(chapterRenderer, 1f, 0f, fadeDuration));

        // 다음 이미지로 교체
        chapterRenderer.sprite = chapterImages[nextIndex];

        // 다음 챕터 페이드인
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
