using System.Collections;
using UnityEngine;
using TMPro;

public class ChapterManager : MonoBehaviour
{
    public static ChapterManager Instance;

    [Header("é�Ϳ� �Բ� ������ ���� ������Ʈ��")]
    [SerializeField] private GameObject[] gameObjectsToFade;

    [Header("é�� �̹���")]
    [SerializeField] private SpriteRenderer chapterSpriteRenderer;

    [Header("��ǰ �̹���")]
    [SerializeField] private SpriteRenderer imageSpriteRenderer;

    [Header("Chapter")]
    [SerializeField] private Sprite[] chapterImages;

    [Header("ItemLayer")]
    [SerializeField] private Sprite[] itemImages;

    [Header("���̵� ȿ��")]
    [SerializeField] private float fadeDuration = 1f;

    [Header("�����۷��̾� ���� �ð�")]
    [SerializeField] private float[] itemDurations = { 1f, 1f, 1f, 1f, 1f };

    [Header("����â")]
    [SerializeField] private SpriteRenderer controlHintSprite; // é��1 ����â (�̹���)
    [SerializeField] private TextMeshPro bossHintText; // é��5 ���� �ؽ�Ʈ
    [SerializeField] private string bossHintContent = "���������� �����Ϸ��� �帲�������� �����ö�";
    [SerializeField] private float hintDuration = 7f;

    public int CurrentChapterIndex { get; private set; } = -1;

    public void Start()
    {
        StartChapterTransition(-1, 0);
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartChapterTransition(int fromChapterIndex, int toChapterIndex)
    {
        if (CurrentChapterIndex == toChapterIndex)
            return;

        StartCoroutine(PlayChaptersWithUpdateIndex(fromChapterIndex, toChapterIndex));
    }

    private IEnumerator PlayChaptersWithUpdateIndex(int fromIndex, int toIndex)
    {
        yield return StartCoroutine(PlayChapters(fromIndex, toIndex));
        CurrentChapterIndex = toIndex;
    }

    private IEnumerator PlayChapters(int fromIndex, int toIndex)
    {
        // é�� �̹��� ���� �� ���̵���
        chapterSpriteRenderer.sprite = chapterImages[toIndex];
        chapterSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        chapterSpriteRenderer.gameObject.SetActive(true);
        yield return StartCoroutine(FadeSpriteRenderer(chapterSpriteRenderer, 0f, 1f, fadeDuration));

        // é�� 1 ���� ���� (�ε��� 0)
        if (toIndex == 0)
        {
            // ������ ���̾�� ���� ���
            yield return StartCoroutine(PlayItemLayers());

            // ���� ������Ʈ ���̵���
            SetGameObjectsAlpha(0f);
            foreach (var obj in gameObjectsToFade)
                obj.SetActive(true);
            yield return StartCoroutine(FadeGameObjects(0f, 1f, fadeDuration));

            // ���� �̹��� ���̵���/����/�ƿ�
            yield return StartCoroutine(ShowHintSprite(controlHintSprite, hintDuration));
        }

        // é�� 5 ���� �ؽ�Ʈ ��Ʈ (�ε��� 4)
        if (toIndex == 4)
        {
            bossHintText.text = bossHintContent;
            bossHintText.alpha = 0f;
            bossHintText.gameObject.SetActive(true);
            yield return StartCoroutine(FadeTextAlpha(bossHintText, 0f, 1f, 1f));
            yield return new WaitForSeconds(hintDuration);
            yield return StartCoroutine(FadeTextAlpha(bossHintText, 1f, 0f, 1f));
            bossHintText.gameObject.SetActive(false);
        }

        // é�� �̹��� ���̵�ƿ�
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FadeSpriteRenderer(chapterSpriteRenderer, 1f, 0f, fadeDuration));
        chapterSpriteRenderer.gameObject.SetActive(false);
    }

    private IEnumerator PlayItemLayers()
    {
        for (int i = 0; i < itemImages.Length; i++)
        {
            imageSpriteRenderer.sprite = itemImages[i];
            imageSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            imageSpriteRenderer.gameObject.SetActive(true);

            yield return StartCoroutine(FadeSpriteRenderer(imageSpriteRenderer, 0f, 1f, fadeDuration));
            yield return new WaitForSeconds(itemDurations[i]);
            yield return StartCoroutine(FadeSpriteRenderer(imageSpriteRenderer, 1f, 0f, fadeDuration));
            imageSpriteRenderer.gameObject.SetActive(false);
        }
    }

    private IEnumerator ShowHintSprite(SpriteRenderer sprite, float duration)
    {
        sprite.color = new Color(1f, 1f, 1f, 0f);
        sprite.gameObject.SetActive(true);
        yield return StartCoroutine(FadeSpriteRenderer(sprite, 0f, 1f, 1f));
        yield return new WaitForSeconds(duration);
        yield return StartCoroutine(FadeSpriteRenderer(sprite, 1f, 0f, 1f));
        sprite.gameObject.SetActive(false);
    }

    private IEnumerator FadeTextAlpha(TextMeshPro text, float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / duration);
            text.alpha = alpha;
            yield return null;
        }
        text.alpha = to;
    }

    private void SetGameObjectsAlpha(float alpha)
    {
        foreach (var obj in gameObjectsToFade)
        {
            foreach (var sr in obj.GetComponentsInChildren<SpriteRenderer>())
            {
                var color = sr.color;
                color.a = alpha;
                sr.color = color;
            }
        }
    }

    private IEnumerator FadeGameObjects(float fromAlpha, float toAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, elapsed / duration);
            SetGameObjectsAlpha(alpha);
            yield return null;
        }
        SetGameObjectsAlpha(toAlpha);
    }

    private IEnumerator FadeSpriteRenderer(SpriteRenderer renderer, float fromAlpha, float toAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = renderer.color;
        color.a = fromAlpha;
        renderer.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(fromAlpha, toAlpha, elapsed / duration);
            renderer.color = color;
            yield return null;
        }

        color.a = toAlpha;
        renderer.color = color;
    }
}
