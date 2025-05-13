using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    void LateUpdate() 
    {
        if (Scorller.globalDirection == 1)
        {
            // 왼쪽 스크롤 → 왼쪽으로 갔으면 오른쪽으로 보내기
            if (transform.position.x > -19) return;
            transform.Translate(53, 0, 0, Space.Self);
        }
        else
        {
            // 오른쪽 스크롤 → 오른쪽으로 갔으면 왼쪽으로 보내기
            if (transform.position.x < 19) return;
            transform.Translate(-53, 0, 0, Space.Self);
        }
    }
}