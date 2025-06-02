using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
    //public Sprite[] images;
    private SpriteRenderer spriteRender;

    void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    //public void Init(int type)
    //{
    //    if (type >= images.Length) return;
    //    spriteRender.sprite = images[type];
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌!");
            PlayerController.instance.TakeDamage();
        }
    }
}