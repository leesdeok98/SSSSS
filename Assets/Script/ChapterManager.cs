using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterManager : MonoBehaviour
{
    [Header("Chapter")]
    [SerializeField] private SpriteRenderer chapterRenderer;
    [SerializeField] private Sprite[] chapterImages;

    [Header("Item")]
    [SerializeField] private SpriteRenderer itemRenderer;
    [SerializeField] private Sprite[] itemImages;

    [Header(" 처음에 페이드인 할 오브젝트들")]
    [SerializeField] private SpriteRenderer[] fadeInObjeccts;

    [Header("페이드 효과")]
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float[] chapterDurations = { 10f, 5f, 7f };
    [SerializeField] private float[] itemrDurations = { 3f, 3f, 3f, 3f, 3f };


    public void Start()
    {
        StartCoroutine(PlayChapters());
    }

    private IEnumerator PlayChapters()
    {
        for (int i = 0; i < chapterImages.Length; i++)
        {
            // 현재 이미지 설정
            chapterRenderer.sprite = chapterImages[i];
            chapterRenderer.color = new Color(1f, 1f, 1f, 0f);
            chapterRenderer.gameObject.SetActive(true);

            // 첫 번째 챕터에서만
            if (i == 0)
            {
                foreach (var obj in fadeInObjeccts)
                {
                    obj.color = new Color(1f, 1f, 1f, 0f);
                    obj.gameObject.SetActive(true);
                    StartCoroutine(Fade(obj, 0f, 1f, fadeDuration));
                }

                yield return StartCoroutine(Fade(chapterRenderer, 0f, 1f, fadeDuration));

                yield return StartCoroutine(PlayItemLayers());
            }
            else
            {
                yield return StartCoroutine(Fade(chapterRenderer, 0f, 1f, fadeDuration));
            }

            // 이미지 유지
            float waitTime = (i < chapterDurations.Length) ? chapterDurations[i] : 5f;
            yield return new WaitForSeconds(waitTime);

            if (i == 0)
            {
                // 동시에 페이드아웃 시작
                Coroutine fadeOutItem = StartCoroutine(Fade(itemRenderer, 1f, 0f, fadeDuration));
                Coroutine fadeOutChapter = StartCoroutine(Fade(chapterRenderer, 1f, 0f, fadeDuration));

                // 동시에 끝날 때까지 기다림
                yield return fadeOutItem;
                yield return fadeOutChapter;

                itemRenderer.gameObject.SetActive(false);
            }
            else
            {
                // 일반적인 챕터 페이드아웃
                yield return StartCoroutine(Fade(chapterRenderer, 1f, 0f, fadeDuration));
            }

            // 마지막 이미지가 아니면 비활성화
            if (i < chapterImages.Length - 1)
            {
                chapterRenderer.gameObject.SetActive(false);
            }
        }
    }
    private IEnumerator PlayItemLayers()
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            itemRenderer.sprite = itemImages[i];
            itemRenderer.color = new Color(1f, 1f, 1f, 0f);
            itemRenderer.gameObject.SetActive(true);

            yield return StartCoroutine(Fade(itemRenderer, 0f, 1f, fadeDuration));
            yield return new WaitForSeconds(itemrDurations[i]);
        }

    }

    /* private IEnumerator WaitForDuration(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    } */

    private IEnumerator Fade(SpriteRenderer renderer, float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = renderer.color;
        color.a = startAlpha;
        renderer.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            color.a = newAlpha;
            renderer.color = color;
            yield return null;
        }

        color.a = endAlpha;
        renderer.color = color;
    }

}
