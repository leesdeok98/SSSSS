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

    // 상태 상수 정의
    const int STATE_IDLE = 0;
    const int STATE_JUMP = 1;
    const int STATE_SLIDE = 2;
    const int STATE_HIT = 3;

    void Start()
    {
        SetGroundTrue();
    }

    void Update()
    {
        if (!isHit) // 피격 중이 아닐 때만 조작 가능
        {
            Slide();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Invoke("TryJump", 0.1f); // 0.1초 딜레이 후 점프
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SetGroundTrue();
            PlayerAnimator.SetInteger("State", STATE_IDLE);
            isGround = true;
            isJumping = false;
            jumpCount = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy") && !isHit)
        {
            Hit();
        }
    }

    void SetGroundTrue()
    {
        isGround = true;
    }

    void TryJump()
    {
        if (isGround)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            PlayerAnimator.SetInteger("State", STATE_JUMP);
            isGround = false;
            isJumping = true;
            jumpCount++;
        }
        else if (isJumping && jumpCount < jumpLevel)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isJumping = false;
            PlayerAnimator.Play("Jump", 0, 0f); // 점프 애니메이션 재시작
        }
        Debug.Log("Jump (delayed)");
    }

    void Slide()
    {
        if (Input.GetKey(KeyCode.C) && isGround)
        {
            PlayerAnimator.SetInteger("State", STATE_SLIDE);
        }
        else if (isGround)
        {
            PlayerAnimator.SetInteger("State", STATE_IDLE);
        }
    }

    void Hit()
    {
        isHit = true;
        PlayerAnimator.SetInteger("State", STATE_HIT);
        Debug.Log("Hit!");

        // 일정 시간 후 피격 상태 해제 및 Idle 복귀
        Invoke("RecoverFromHit", 2.0f); // Hit 애니메이션 길이에 맞게 조절
    }

    void RecoverFromHit()
    {
        isHit = false;
        PlayerAnimator.SetInteger("State", STATE_IDLE);
    }
}
