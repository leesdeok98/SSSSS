using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager2 : MonoBehaviour
{
    private bool isMoving = false;

    void Start()
    {
        Invoke("StartMoving", 75f);
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.right * SpeedManager.Instance.moveSpeed * Time.deltaTime);
        }
    }

    void StartMoving()
    {
        isMoving = true;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
