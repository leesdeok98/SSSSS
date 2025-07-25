using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class storyManager : MonoBehaviour
{
    [Header("이미지")]
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;

    [Header("스크립터블")]
    [Space(10)]
    [Tooltip("성덕이형 에바참치꽁치")]
    [SerializeField] private ImageChanger[] images;

    [Header("타이핑 효과 설정")]
    [SerializeField] private float typingSpeed = 0.05f;

    private int currentImage = 0;
    private Coroutine typingCorutine;

    private enum StoryStep
    {
        WaitForImage,
        WaitForText,
        //TypingText,
        TextComplete,
        StoryEnd
    }

    private StoryStep currentStep = StoryStep.WaitForImage;

    private void Start()
    {
        text.text = "";

        if (images.Length > 0 && FadeEffect.Instance != null)
        {
            image.sprite = images[0].storyImage;
            StartCoroutine(FadeEffect.Instance.FadeIn());
            currentStep = StoryStep.WaitForText;
        }

        AudioManager.instance.UnPauseBGM(); // 메인 타이틀 음악 재생
    }

    private bool isTyping = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentStep)
            {
                case StoryStep.WaitForText:
                    if (!isTyping) // 이미 타이핑 중이면 무시
                    {
                        StartTyping();
                        SoundManager.Instance.Play("Typing");
                    }
                    break;

                /*case StoryStep.TypingText:
                    CompleteTyping();
                    break; */

                case StoryStep.TextComplete:
                    StartCoroutine(FadeOutToNextImage());
                    break;

                case StoryStep.WaitForImage:
                    // 자동 호출되므로 사용자 입력 없음
                    break;

                case StoryStep.StoryEnd:
                    // 끝났으므로 아무 동작 없음
                    break;
            }
        }
    }

    private void ShowNextImage()
    {
        image.sprite = images[currentImage].storyImage;
        text.text = "";

        if (FadeEffect.Instance != null)
        {
            StartCoroutine(FadeEffect.Instance.FadeIn());
        }

        currentStep = StoryStep.WaitForText;
    }

     void StartTyping()
     {
        if (typingCorutine != null)
            StopCoroutine(typingCorutine); // 혹시 이전 코루틴이 남아 있다면 정지

        isTyping = true;
        
        typingCorutine = StartCoroutine(TypeText(images[currentImage].storyText));
        //currentStep = StoryStep.TypeText;
     }

    private IEnumerator TypeText(string storyText)
    {
        for (int i = 0; i <= storyText.Length; i++)
        {
            text.text = storyText.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false; // 타이핑 완료됨
        currentStep = StoryStep.TextComplete;
    }

    /* private void CompleteTyping()
    {
        if (typingCorutine != null)
            StopCoroutine(typingCorutine);

        text.text = images[currentImage].storyText;
        currentStep = StoryStep.TextComplete;
    } */

    private IEnumerator FadeOutToNextImage()
    {
        if (FadeEffect.Instance != null)
        {
            yield return StartCoroutine(FadeEffect.Instance.FadeOut());
        }

        currentImage++;

        if (currentImage >= images.Length)
        {
            currentStep = StoryStep.StoryEnd;
            SoundManager.Instance.Stop("Typing");
            StartCoroutine(FadeOutAndLoadScene("NanEdo"));
        }
        else
        {
            ShowNextImage();
        }
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        // 이미 페이드아웃된 상태라고 가정
        yield return new WaitForSeconds(0.5f); // 짧은 텀
        AudioManager.instance.StopBGM();
        SceneManager.LoadScene(sceneName);
        
    }
}