using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 이동을 위한 네임스페이스 추가

public class PlayerMove : MonoBehaviour
{
    private int lives = 3;
    private bool isInvincible = false;
    private bool isHit = false;
    private State state;

    public enum State { Stand, Run, Jump, Hit, Slide }
    public int jumpLevel = 2;
    public float maxSpeed;
    public float JumpPower;
    public bool isGround;
    public BoxCollider2D SlcCol;
    public BoxCollider2D RunnCol;
    private int jumpCount = 0;
    private bool isSliding = false;

    private Rigidbody2D rigid;
    private Animator anim;
   

    
   

    void Awake()
    {
        SlcCol.enabled=false;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
 
   
    }

    void Start()
    {
        isSliding = false;
        ChangeAnim(State.Run);
    }

    void Update()
    {
        Debug.Log(isSliding);


        if (isHit)
            return;
 
        HandleSlide();
        HandleJump();

    }
    private void LateUpdate()
    {
        anim.SetInteger("State", (int)state);
        anim.SetBool("isSliding", isSliding);
    }

    

   

   

    void Jump()
    {
        isGround = false;
        Debug.Log("Jump");
        rigid.velocity = new Vector2(rigid.velocity.x, 0f);
        rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        ChangeAnim(State.Jump);
        
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
       
            if (isGround && jumpCount == 0)
            {
                Jump();
                jumpCount = 1;
            }
            else if (!isGround && jumpCount <= jumpLevel)
            {
                Jump();
                jumpCount++;
            }
        }
    }

    void HandleSlide()
    {
    
        if (Input.GetKey(KeyCode.C) && isGround)
        {  
            isSliding = true;
            RunnCol.enabled= false;
            SlcCol.enabled = true;
            ChangeAnim(State.Slide);
        }
        else
        {
            isSliding = false;
            RunnCol.enabled = true;
            SlcCol.enabled = false;
            if (isGround == true)
                ChangeAnim(State.Run);

        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        isGround = true;
        jumpCount = 0;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("탈출");
        isGround = false;
    }

    void ChangeAnim(State state)
    {

        if (isHit && state != State.Hit) return;
        this.state = state;
        
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
        if (isInvincible) return;

        isHit = true;
        lives -= 1;
        ChangeAnim(State.Hit);
        StartInvincible();
        Debug.Log("Crush");

        if (lives <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Game over");
    }

    void StartInvincible()
    {
        isInvincible = true;
        Invoke("StopInvincible", 0.5f);
    }

    void StopInvincible()
    {
        isInvincible = false;
        isHit = false;
    }
}






