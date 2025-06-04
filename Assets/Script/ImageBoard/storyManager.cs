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
        TypingText,
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentStep)
            {
                case StoryStep.WaitForText:
                    StartTyping();
                    break;

                case StoryStep.TypingText:
                    CompleteTyping();
                    break;

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

    private void StartTyping()
    {
        typingCorutine = StartCoroutine(TypeText(images[currentImage].storyText));
        currentStep = StoryStep.TypingText;
    }

    private IEnumerator TypeText(string storyText)
    {
        for (int i = 0; i <= storyText.Length; i++)
        {
            text.text = storyText.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }

        currentStep = StoryStep.TextComplete;
    }

    private void CompleteTyping()
    {
        if (typingCorutine != null)
            StopCoroutine(typingCorutine);

        text.text = images[currentImage].storyText;
        currentStep = StoryStep.TextComplete;
    }

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
            StartCoroutine(FadeOutAndLoadScene("GameView"));
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
        SceneManager.LoadScene(sceneName);
    }
}