using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveing : MonoBehaviour
{
    [Header("Settings")]
    public float JumpForce;

    [Header("References")]
    public Rigidbody2D rb;
    public Animator PlayerAnimator;

    private bool isGround = false;
    private int jumpCount = 0;
    private bool isJumping = false;
    public int jumpLevel = 2;
    private bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        SetGroundTrue();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit) return; // 피격 중엔 조작 금지

        Slide();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("TryJump", 0.1f); // 0.1초 딜레이 후 점프 시도
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SetGroundTrue();
            PlayerAnimator.SetInteger("State", 0);
            isGround = true;
            isJumping = false;
            jumpCount = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy") && !isHit)
        {
            Hit();
        }
    }

    void Hit()
    {
        isHit = true;
        PlayerAnimator.SetInteger("State", 3); // Hit 애니메이션 재생
        rb.velocity = Vector2.zero; // 피격 시 멈추기

        Debug.Log("Crush");

        Invoke("RecoverFromHit", 0.7f); // 0.7초 후 회복
    }

    void RecoverFromHit()
    {
        isHit = false;
        PlayerAnimator.SetInteger("State", 0); // 다시 Idle 상태로
    }

    void SetGroundTrue()
    {
        isGround = true;
    }

    void TryJump()
    {
        if (isHit) return; // 피격 중 점프 금지

        if (isGround)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            PlayerAnimator.SetInteger("State", 1);
            isGround = false;
            isJumping = true;
            jumpCount++;
        }
        else if (isJumping && jumpCount < jumpLevel)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isJumping = false;
            Animator animator = GetComponent<Animator>();
            animator.Play("Jump", 0, 0f);
        }

        Debug.Log("Jump (delayed)");
    }

    void Slide()
    {
        if (isHit) return; // 피격 중 슬라이드 금지

        if (Input.GetKey(KeyCode.C) && isGround)
        {
            PlayerAnimator.SetInteger("State", 2);
        }
        else if (isGround)
        {
            PlayerAnimator.SetInteger("State", 0);
        }
    }
}
