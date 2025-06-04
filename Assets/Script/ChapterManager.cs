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

    [Header(" ó���� ���̵��� �� ������Ʈ��")]
    [SerializeField] private SpriteRenderer[] fadeInObjeccts;

    [Header("���̵� ȿ��")]
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
            // ���� �̹��� ����
            chapterRenderer.sprite = chapterImages[i];
            chapterRenderer.color = new Color(1f, 1f, 1f, 0f);
            chapterRenderer.gameObject.SetActive(true);

            // ù ��° é�Ϳ�����
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

            // �̹��� ����
            float waitTime = (i < chapterDurations.Length) ? chapterDurations[i] : 5f;
            yield return new WaitForSeconds(waitTime);

            if (i == 0)
            {
                // ���ÿ� ���̵�ƿ� ����
                Coroutine fadeOutItem = StartCoroutine(Fade(itemRenderer, 1f, 0f, fadeDuration));
                Coroutine fadeOutChapter = StartCoroutine(Fade(chapterRenderer, 1f, 0f, fadeDuration));

                // ���ÿ� ���� ������ ��ٸ�
                yield return fadeOutItem;
                yield return fadeOutChapter;

                itemRenderer.gameObject.SetActive(false);
            }
            else
            {
                // �Ϲ����� é�� ���̵�ƿ�
                yield return StartCoroutine(Fade(chapterRenderer, 1f, 0f, fadeDuration));
            }

            // ������ �̹����� �ƴϸ� ��Ȱ��ȭ
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
