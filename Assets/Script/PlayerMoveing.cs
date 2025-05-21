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
        if (isHit) return; // �ǰ� �߿� ���� ����

        Slide();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("TryJump", 0.1f); // 0.1�� ������ �� ���� �õ�
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
        PlayerAnimator.SetInteger("State", 3); // Hit �ִϸ��̼� ���
        rb.velocity = Vector2.zero; // �ǰ� �� ���߱�

        Debug.Log("Crush");

        Invoke("RecoverFromHit", 0.7f); // 0.7�� �� ȸ��
    }

    void RecoverFromHit()
    {
        isHit = false;
        PlayerAnimator.SetInteger("State", 0); // �ٽ� Idle ���·�
    }

    void SetGroundTrue()
    {
        isGround = true;
    }

    void TryJump()
    {
        if (isHit) return; // �ǰ� �� ���� ����

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
        if (isHit) return; // �ǰ� �� �����̵� ����

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
