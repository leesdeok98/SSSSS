using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float speed = 5f;
    private float destroyX = -15f;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x <= destroyX)
        {
            Destroy(gameObject);
        }
    }
}
