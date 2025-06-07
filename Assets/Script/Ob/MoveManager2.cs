using System.Collections;
using UnityEngine;

public class MoveManager2 : MonoBehaviour
{
    private Vector2 moveDirection;
    public float speed = 10f;
    public bool startFromRight = false;

    private bool canMove = false;

    void Start()
    {
        moveDirection = startFromRight ? Vector2.left : Vector2.right;
        Invoke("StartMoving", 85f);  // 75초 뒤 이동 시작
    }

    void StartMoving()
    {
        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            transform.Translate(moveDirection * SpeedManager.Instance.moveSpeed * Time.deltaTime);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
