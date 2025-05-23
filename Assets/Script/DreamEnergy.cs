using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamEnergy : MonoBehaviour
{
    public static DreamEnergy instance;

    public float moveSpeed = 10f;        
    private float destroyX = -15f;      

    private void Awake()
    {
        if (instance == null) instance = this;
    }


    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        if (transform.position.x <= destroyX)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.AddDreamEnergy(); // ÄÚÀÎ X
            Destroy(gameObject);
        }
    }
}
