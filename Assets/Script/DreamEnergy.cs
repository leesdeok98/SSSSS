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
       Invoke("Move", 1f); // Move 메서드를 주기적으로 호출
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger 감지됨: " + other.name);
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.AddDreamEnergy(); // 코인 X
            AudioManager.instance.PlaySFX(AudioManager.SFX.DE); //  SFX 재생
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
