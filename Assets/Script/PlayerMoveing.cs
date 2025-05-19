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

private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            PlayerAnimator.SetInteger("State", 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            PlayerAnimator.SetInteger("State", 0);
        }
    }
}
