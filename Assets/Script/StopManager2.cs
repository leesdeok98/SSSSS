using UnityEngine;

public class StopManager2 : MonoBehaviour
{
    public float speed = 10f;
    public bool startFromRight = false;  // ✅ Inspector에서 체크해서 방향 제어

    private Vector2 moveDirection;

    void Start()
    {
        moveDirection = startFromRight ? Vector2.left : Vector2.right;
    }

    void Update()
    {
        Invoke("Move", 75f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveDirection = -moveDirection;
            Debug.Log("플레이어와 충돌! 이동 방향 반전");
        }
    }
    void Move()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}
