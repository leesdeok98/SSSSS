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
        chapterRenderer.sprite = chapterImages[0];
        SetAlpha(chapterRenderer, 0f);
        chapterRenderer.gameObject.SetActive(true);

        StartCoroutine(StartFirstChapter());
    }

    private IEnumerator StartFirstChapter()
    {
        AudioManager.instance.PlayBGM(1);

        StartCoroutine(AudioManager.instance.MusicFadein());
        yield return StartCoroutine(Fade(chapterRenderer, 0f, 1f, fadeDuration));
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
        StartCoroutine(AudioManager.instance.MusicFadeout());
        yield return StartCoroutine(Fade(chapterRenderer, 1f, 0f, fadeDuration));

        chapterRenderer.sprite = chapterImages[nextIndex];

        StartCoroutine(AudioManager.instance.MusicFadein());
        yield return StartCoroutine(Fade(chapterRenderer, 0f, 1f, fadeDuration));
    }

    public void GoToChapter(int chapterIndex)
    {
        if (chapterIndex >= 0 && chapterIndex < chapterImages.Length)
        {
            StartCoroutine(ChangeToSpecificChapter(chapterIndex));
            currentChapterIndex = chapterIndex;
        }
    }

    private IEnumerator ChangeToSpecificChapter(int chapterIndex)
    {
        StartCoroutine(AudioManager.instance.MusicFadeout());
        yield return StartCoroutine(Fade(chapterRenderer, 1f, 0f, fadeDuration));

        AudioManager.instance.PlayBGM(chapterIndex + 1);
        chapterRenderer.sprite = chapterImages[chapterIndex];

        StartCoroutine(AudioManager.instance.MusicFadein());
        yield return StartCoroutine(Fade(chapterRenderer, 0f, 1f, fadeDuration));
    }

    public void GoToMainTitle()
    {
        StartCoroutine(ChangeToMainTitle());
    }

    private IEnumerator ChangeToMainTitle()
    {
        StartCoroutine(AudioManager.instance.MusicFadeout());
        yield return StartCoroutine(Fade(chapterRenderer, 1f, 0f, fadeDuration));

        AudioManager.instance.PlayBGM(0);

        StartCoroutine(AudioManager.instance.MusicFadein());

        currentChapterIndex = -1;
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

    public int GetCurrentChapter()
    {
        return currentChapterIndex;
    }

    public void SetCurrentChapterBGMVolume(float volume)
    {
        if (currentChapterIndex >= 0)
        {
            AudioManager.instance.SetBGMVolume(currentChapterIndex + 1, volume);
        }
    }

    public void SetMasterBGMVolume(float volume)
    {
        AudioManager.instance.SetMasterBGMVolume(volume);
    }
}