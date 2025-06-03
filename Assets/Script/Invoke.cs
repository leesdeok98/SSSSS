using System.Collections;
using UnityEngine;

public class Invoke : MonoBehaviour
{
    public float speed = 10f;
    private bool canMove = false;

    void Start()
    {
        Invoke("StartMoving", 100f);  // 75초 뒤 이동 시작
    }

    void StartMoving()
    {
        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime); // 항상 왼쪽으로 이동
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
