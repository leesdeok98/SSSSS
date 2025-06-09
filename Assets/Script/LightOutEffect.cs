using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LightOutEffect : MonoBehaviour
{
    public Canvas uiCanvas;                      // 깜빡임을 띄울 캔버스
    public float blackHoldTime = 0.2f;           // 페이드 인/아웃 시간
    public float blackStayTime = 0.1f;           // 어두운 상태 유지 시간
    public Color flashColor = Color.black;       // 깜빡이는 색

    private bool isFlashing = false;
    public GameObject Boss;
    public string targetTag = "Chapter";

    // 깜빡임 지속 시간 패턴 (초단위) – 느린 구간 2개 2.5초로 추가
    private readonly List<float> flashPattern = new List<float> {
        0.03f,0.03f,1.5f,0.03f,0.03f,1.5f
    };



    public IEnumerator FlashLoop()
    {
        isFlashing = true;

        // 화면 전체를 덮는 Image 생성
        GameObject imgObj = new GameObject("FlashImage", typeof(Image));
        imgObj.transform.SetParent(uiCanvas.transform, false);

        Image flashImage = imgObj.GetComponent<Image>();
        flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);

        RectTransform rect = flashImage.rectTransform;
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        // 깜빡임 패턴 실행
        foreach (float duration in flashPattern)
        {
            // 페이드 인
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / blackHoldTime;
                float alpha = Mathf.Lerp(0f, 1f, t);
                flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
                yield return null;
            }

            // 어두운 상태 유지
            flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 1f);
            yield return new WaitForSeconds(duration);

            // 페이드 아웃
            t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / blackHoldTime;
                float alpha = Mathf.Lerp(1f, 0f, t);
                flashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
                yield return null;
            }

            yield return new WaitForSeconds(0.05f); // 깜빡임 간 간격
        }

        Destroy(imgObj);
        isFlashing = false;
    }
}

