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
    private int jumpCount  = 0;
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
        Slide();
        Jump();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SetGroundTrue();
            //Invoke("SetGroundTrue", 0.05f); // 0.05초 후에 isGround = true 실행
            PlayerAnimator.SetInteger("State", 0);
            isGround = true;
            isJumping = false;
            jumpCount = 0;
        }

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Hit();
        }
    }

    void Hit()
    {
        
        isHit = true;
        PlayerAnimator.SetInteger("State", 4);

        Debug.Log("Crush");
 
    }

    void SetGroundTrue()
    {
        isGround = true;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
        }

    }

    void Slide()
    {
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
