using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Sprite[] images;
    SpriteRenderer spriteRender;
    void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    public void Init(int type)
    {
        if(type >= images.Length)
            return;
        spriteRender.sprite = images[type];
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌!");
            Destroy(other.gameObject); // 또는 데미지 처리
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // 화면 밖으로 나가면 삭제
    }
}