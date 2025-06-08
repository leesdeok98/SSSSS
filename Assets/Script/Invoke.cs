using System.Collections;
using UnityEngine;

public class Invoke : MonoBehaviour
{
    public float speed = 10f;
    private bool canMove = false;

    void Start()
    {
        Invoke("StartMoving", 110f);  // 75�� �� �̵� ����
    }

    void StartMoving()
    {
        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector2.left * SpeedManager.Instance.moveSpeed * Time.deltaTime); // �׻� �������� �̵�
        }
    }

    void OnBecameInvisible()
    {
        // 10�� �ڿ� Destroy ����
        Invoke("DestroySelf", 0.5f);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
