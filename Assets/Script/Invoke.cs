using System.Collections;
using UnityEngine;

public class Invoke : MonoBehaviour
{
    public float speed = 10f;
    private bool canMove = false;

    void Start()
    {
        Invoke("StartMoving", 100f);  // 75�� �� �̵� ����
    }

    void StartMoving()
    {
        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime); // �׻� �������� �̵�
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
