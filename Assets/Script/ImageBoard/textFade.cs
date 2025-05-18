/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextFade : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        StartCoroutine(FadeTextToFullAlpha());

    }

    public IEnumerator FadeTextToFullAlpha() //알파값 0에서 1로 바뀜
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            float newAlpha = text.color.a + Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Clamp01(newAlpha));
            yield return null;
        }

        StartCoroutine(FadeTextToZero());
    }

    public IEnumerator FadeTextToZero() //알파값 1에서 0으로 바뀜
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            float newAlpha = text.color.a - Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Clamp01(newAlpha));
            yield return null;
        }

        StartCoroutine(FadeTextToFullAlpha());
    }
} */