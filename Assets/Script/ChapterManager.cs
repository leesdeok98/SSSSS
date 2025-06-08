using System.Collections;
using UnityEngine;
using TMPro;

public class ChapterManager : MonoBehaviour
{
    [Header("챕터")]
    [SerializeField] private SpriteRenderer chapterRenderer;
    [SerializeField] private Sprite[]       chapterImages;
    [SerializeField] private float          fadeDuration = 1f;

    [Header("소품 (1챕터)")]
    [SerializeField] private SpriteRenderer itemRenderer;              // 하나의 SpriteRenderer만 사용
    [SerializeField] private Sprite[]       itemImages;                // 순서대로 보여줄 아이템 스프라이트들
    [SerializeField] private float[]        itemDurations;             // 각 이미지가 유지될 시간 (이미지 수와 동일 길이 필요)

    [Header("설명창")]
    [SerializeField] private SpriteRenderer controlRenderer;           // 챕터 1 -> 조작키 설명창
    [SerializeField] private TextMeshPro    bossHintRenderer;          // 챕터 3 -> 드림에너지(보스 관련) 설명창
    [SerializeField] private float          controlDuration;           // 조작키 설명창 유지시간
    [SerializeField] private float          hintDuration;              // 드림에너지 설명창 유지시간

    [Header("보스")]
    [SerializeField] private SpriteRenderer BossRenderer;


    private int currentChapterIndex = 0;


    private void Start()
    {
        chapterRenderer.sprite = chapterImages[0];
        SetAlpha(chapterRenderer, 0f);
        chapterRenderer.gameObject.SetActive(true);

        StartCoroutine(StartFirstChapter());
    }

    private IEnumerator StartFirstChapter()  // 첫 번재 챕터 시작
    {
        AudioManager.instance.PlayBGM(1);

        StartCoroutine(AudioManager.instance.MusicFadein());

        // 페이드인 -> 이거 키면 챕터1 이미지 페이드인이 두 번 실행됨
        //yield return StartCoroutine(Fade(chapterRenderer, 0f, 1f, fadeDuration));

        if (currentChapterIndex == 0)
        {
            // 아이템 첫 이미지 설정 및 초기화
            itemRenderer.sprite = itemImages[0];
            itemRenderer.color = new Color(1f, 1f, 1f, 0f);
            itemRenderer.gameObject.SetActive(true);

            //controlRenderer.color = new Color(1f, 1f, 1f, 0f);
            controlRenderer.gameObject.SetActive(true);
            
            // 챕터 이미지, 설명창, 아이템 레이어 동시에 페이드인 시작
            Coroutine chapterFadeCoroutine = StartCoroutine(Fade(chapterRenderer, 0f, 1f, fadeDuration));
            Coroutine controlFadeCoroutine = StartCoroutine(Fade(controlRenderer, 0f, 1f, fadeDuration));
            Coroutine itemFadeCoroutine = StartCoroutine(Fade(itemRenderer, 0f, 1f, fadeDuration));

            yield return chapterFadeCoroutine;
            yield return controlFadeCoroutine;
            yield return itemFadeCoroutine;

            // 설명창 관련
            yield return new WaitForSeconds(controlDuration);
            StartCoroutine(Fade(controlRenderer, 1f, 0f, fadeDuration));

            // 아이템 레이어 관련 (설명창이 페이드아웃 되기 전에도 작동 해야됨)
            yield return StartCoroutine(PlayItemLayers());
            
        }

        else
        {
            yield return StartCoroutine(Fade(chapterRenderer, 0f, 1f, fadeDuration));
        }

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

    private IEnumerator ChangeChapter(int nextIndex)    // 챕터 이미지 변경
    {
        if (currentChapterIndex == 0 && itemRenderer.gameObject.activeSelf) // 챕터1에서 아이템 레이어 페이드아웃 효과
        {
            StartCoroutine(Fade(itemRenderer, itemRenderer.color.a, 0f, fadeDuration));
        }

        StartCoroutine(AudioManager.instance.MusicFadeout());
        yield return StartCoroutine(Fade(chapterRenderer, 1f, 0f, fadeDuration));

        if (currentChapterIndex == 2) //동시 실행
        {
            StartCoroutine(BossFadeOut());
        }

        // 다음 이미지 설정
        AudioManager.instance.PlayBGM(nextIndex + 1);
        chapterRenderer.sprite = chapterImages[nextIndex];

        StartCoroutine(AudioManager.instance.MusicFadein());
        yield return StartCoroutine(Fade(chapterRenderer, 0f, 1f, fadeDuration));

        if (currentChapterIndex == 1)  // 챕터2에서 보스 관련 설명창 (동시 실행)
        {
            StartCoroutine(BossFadeIn());
            yield return new WaitForSeconds(1f);
            StartCoroutine(PlayBossHint());
        }

        if (currentChapterIndex == 2) // 동시실행
        {
            StartCoroutine(BossFadeIn());
        }
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

    private IEnumerator BossFadeIn()    //보스 페이드인
    {
        yield return StartCoroutine(Fade(BossRenderer, 0f, 1f, fadeDuration));
    }

    private IEnumerator BossFadeOut()    // 보스 페이드아웃
    {
        yield return StartCoroutine(Fade(BossRenderer, 1f, 0f, fadeDuration));
    }

    private IEnumerator PlayItemLayers() // 소품 레이어
    {
        for (int i = 1; i < itemImages.Length; i++)
        {
            itemRenderer.sprite = itemImages[i];                                     // 현재 이미지로 설정
            itemRenderer.color = new Color(1f, 1f, 1f, 0f);                          // 완전 투명으로 시작
            //itemRenderer.gameObject.SetActive(true);                                 // 렌더러 활성화

            yield return StartCoroutine(Fade(itemRenderer, 0f, 1f, fadeDuration));   // 페이드인
            yield return new WaitForSeconds(itemDurations[i]);                       // 지정된 시간 동안 유지
        }
    }

    private IEnumerator PlayBossHint()
    {
        bossHintRenderer.color = new Color(1f, 1f, 1f, 0f);  // 투명으로 시작
        bossHintRenderer.gameObject.SetActive(true); // 오브젝트 활성화

        // 페이드인
        yield return StartCoroutine(textFade(0f, 1f, fadeDuration));

        //지정된 시간 동안 유지
        yield return new WaitForSeconds(hintDuration);

        //페이드아웃
        yield return StartCoroutine(textFade(1f, 0f, fadeDuration));
        bossHintRenderer.gameObject.SetActive(false); // 오브젝트 비활성화

    }

    private IEnumerator PlayControlImage() // 조작키 설명창 (챕터 1에서 나오는 Image)
    {
        //  controlRenderer.color = new Color(1f, 1f, 1f, 0f); // 투명화
        controlRenderer.gameObject.SetActive(true); // 활성화

        //페이드인
        yield return StartCoroutine(Fade(controlRenderer, 0f, 1f, fadeDuration));

        // 지정된 시간 동안 유지
        yield return new WaitForSeconds(controlDuration);

        //페이드아웃
        yield return StartCoroutine(Fade(controlRenderer, 1f, 0f, fadeDuration));

    }

    private IEnumerator Fade(SpriteRenderer renderer, float fromAlpha, float toAlpha, float duration)    // 스프라이트 페이드 효과
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

    private IEnumerator textFade(float fromAlpha, float toAlpha, float duration)  // 드림에너지 설명창에서 필요한 텍스트 페이드효과
    {
        float time = 0f;
        Color color = bossHintRenderer.color;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, time / duration);
            color.a = alpha;
            bossHintRenderer.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        color.a = toAlpha;
        bossHintRenderer.color = color;
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