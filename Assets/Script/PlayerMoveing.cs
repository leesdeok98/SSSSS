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

    // ���� ��� ����
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
        if (!isHit) // �ǰ� ���� �ƴ� ���� ���� ����
        {
            Slide();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Invoke("TryJump", 0.1f); // 0.1�� ������ �� ����
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
            PlayerAnimator.Play("Jump", 0, 0f); // ���� �ִϸ��̼� �����
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

        // ���� �ð� �� �ǰ� ���� ���� �� Idle ����
        Invoke("RecoverFromHit", 2.0f); // Hit �ִϸ��̼� ���̿� �°� ����
    }

    void RecoverFromHit()
    {
        isHit = false;
        PlayerAnimator.SetInteger("State", STATE_IDLE);
    }
}
