using UnityEngine;

public class StopManager : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveDirection = Vector2.left;

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveDirection = -moveDirection;
            Debug.Log("플레이어와 충돌! 이동 방향 반전");
        }
    }
    

}