using System.Collections;
using UnityEngine;

public class Invoke : MonoBehaviour
{
    public float speed = 10f;
    private bool canMove = false;

    void Start()
    {
        Invoke("StartMoving", 110f);  // 75초 뒤 이동 시작
    }

    void StartMoving()
    {
        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector2.left * SpeedManager.Instance.moveSpeed * Time.deltaTime); // 항상 왼쪽으로 이동
        }
    }

    void OnBecameInvisible()
    {
        // 10초 뒤에 Destroy 실행
        Invoke("DestroySelf", 0.5f);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
