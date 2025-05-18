using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    [Header("이미지")]
    [SerializeField] private Image image; //SerializeField : private를 public으로 만들어주는 거
    [SerializeField] private Image textimage;
    [SerializeField] private TMP_Text text;

    [Header("스크립터블")]
    [Space(10)] //위에 글이랑 아래 글 거리
    [Tooltip(" 성덕이형 에바참치꽁치")]
    [SerializeField] private ImageChanger[] images;
    [Header("타이핑 효과 설정")]
    [SerializeField] private float typingSpeed = 0.05f;

    private bool isFading = true;
    private bool isTyping = false;
    private int currentImage = 0;
    private Coroutine typingCorutine;


   
    private void Start()
    {
        //초기 설정
        isFading = true;
        text.text = "";
        if (images.Length > 0 && FadeEffect.Instance != null )
        {
            //시작 시, 첫번째 이미지 로드
            image.sprite = images[0].storyImage;

            // Fade In 효과로 시작
            StartCoroutine(FadeEffect.Instance.Fade(1, 0));
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           if (isFading)
            {
                //Space 첫 호출 시, 이미지 및 텍스트 설정 후 타이핑 시작
                ShowNextImage();
            }
           else if (isTyping)
            {
                // Space 두번째 호출 시, 이미 타이핑 중이면 타이핑 즉시 완료
                CompIeteTyping();
            }
           else
            {
                // 타이핑 완료 후 Space 호출 시, 다음 이미지로 전환 준비
                isFading = true;
            }
        }
    }  
    private void ShowNextImage()
    {
        if(currentImage >= images.Length)
        {
            return;
        }
        if (FadeEffect.Instance != null)
        {
            StartCoroutine(FadeEffect.Instance.Fade(1, 0));
        }
        else
        { 
            
        }

        // 이미지 설정
        image.sprite = images[currentImage].storyImage;

        // 기존 텍스트 초기화 후 타이핑 시작
        text.text = "";
        typingCorutine = StartCoroutine(TypeText(images[currentImage].storyText));

        //상태 변경
        isFading = false;
        isTyping = true;

        //다음 이미지 준비
        currentImage++;
    }

    private IEnumerator TypeText(string storyText)
    {
        // 타이핑 효과 적용
        for (int i = 0; i <= storyText.Length; i++)
        {
            text.text = storyText.Substring(0,i);
            yield return new WaitForSeconds(typingSpeed);
        }

        // 타이핑 완료
        isTyping = false;
    }
    private void CompIeteTyping()
    {

    }
}
