using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamEnergy : MonoBehaviour
{
    public static DreamEnergy instance;

    //public float moveSpeed = 10f;        
    private float destroyX = -15f;      

    private void Awake()
    {
        if (instance == null) instance = this;
    }


    void Update()
    {
       Invoke("Move", 1f); // Move �޼��带 �ֱ������� ȣ��
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger ������: " + other.name);
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.AddDreamEnergy(); // ���� X
            AudioManager.instance.PlaySFX(AudioManager.SFX.DE); //  SFX ���
            Destroy(gameObject);
        }
    }
    //void Move()
    //{
    //    transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

    //    if (transform.position.x <= destroyX)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
