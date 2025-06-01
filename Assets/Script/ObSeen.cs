using UnityEngine;

public class ObSeen : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveDirection = Vector2.left;

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }
}