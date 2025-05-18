using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public static FadeEffect Instance;
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime; //fadeSpeed 값이 10이면 1초 (값이 클수록 빠름)
    private Image image; //페이드 효과에 사용되는 검은 바탕 이미지
    bool fadetime;

    private void Awake()
    {
        Instance = this;
        image = GetComponent<Image>();

        //StartCoroutine(Fade(1, 0)); // Fade In : 배경의 알파값이 1에서 0 (화면이 점점 밝아짐)
    }

    private void Update()
    {
        if (fadetime)
        {
            //image의 color 프로퍼티는 a 변수만 따로 set이 불가능해서 변수에 저저장
            Color color = image.color;

            // 알파 값(a)이 0보다 크면 알파 값 감소
            if (color.a > 0)
            {
                color.a -= Time.deltaTime;
            }

            // 바뀐 색상 정보를 image.color에 저장
            image.color = color;
        }
    }

    public IEnumerator Fade(float start, float end)
    {
        fadetime = true;
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime; //fadeTime으로 나누어서 fadeTime 시간 동안
            percent = currentTime / fadeTime; //percent 값이 0에서 1로 증가하도록 함

            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            fadetime = false;
            yield return null;
        }
    }
}