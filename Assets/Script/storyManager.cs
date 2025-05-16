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

    private bool fadeimagetime;

    public int currentImage;

    private void Awake()
    {
        fadeimagetime = true;
    }
    void Update()
    {
        NextStory();
    }

    private void NextStory()
    {
        if (Input.GetKeyDown(KeyCode.Space) && fadeimagetime)
        {
            if (FadeEffect.Instance != null)
                StartCoroutine(FadeEffect.Instance.Fade(1, 0));

            else
                Debug.LogWarning("FadeEffect.Instance가 null입니다!");

                image.sprite = images[currentImage].storyImage;
                text.text = images[currentImage].storyText;
                currentImage++;

                fadeimagetime = false;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && !fadeimagetime)
        {
            text.GetComponent<typingEffect>();

            fadeimagetime = true;
        }
    }
} 