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
    public float maxSpeed = 5f; // 달리기 속도
    public float JumpPower;
    public bool isGround;
    public BoxCollider2D SlcCol;
    public BoxCollider2D RunnCol;
    private int jumpCount = 0;
    private bool isSliding = false;
    private bool isRunningToEdge = false;  // P키 달리기 상태
    private bool isJumping = false;

    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        SlcCol.enabled = false;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        isSliding = false;
        ChangeAnim(State.Run);
    }

    void Update()
    {
        // P키 입력 처리
        if (Input.GetKeyDown(KeyCode.P) && !isRunningToEdge && isGround)
        {
            StartCoroutine(RunToRightEdge());
        }

        // 충돌하지 않으면 점프와 슬라이드 처리
        if (!isRunningToEdge && !isHit)
        {
            HandleSlide();
            HandleJump();
            if (!isJumping && !isSliding)
            {
                ChangeAnim(State.Run);
            }
        }

    }

    private void LateUpdate()
    {
        //if (!isHit && !isSliding && !isRunningToEdge)
        //{
        //    if (isGround && state != State.Run)
        //        ChangeAnim(State.Run);
        //}

        anim.SetInteger("State", (int)state);
        anim.SetBool("isSliding", isSliding);
    }

    void Jump()
    {
        isJumping = true;
        Debug.Log("Jump");
        rigid.velocity = new Vector2(rigid.velocity.x, 0f);
        rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        ChangeAnim(State.Jump);
    }

    void HandleJump()
    {
        //스페이스바를 누르면 땅일때 점프를 한다
        if (Input.GetKeyDown(KeyCode.Space))
        {   //현재 위치가 땅이며 점프카운트가 0일때? 점프상태가 아닐때
            if (isGround && jumpCount == 0 && !isJumping)
            {
                Jump();
                jumpCount = 1;
            }
            //점프카운트가 1이상이고 플레이어가 땅에 없을때 한번더 점프
            else if (isJumping && jumpCount <= jumpLevel)
            {
                Jump();
                jumpCount++;
                Animator animator = GetComponent<Animator>();
                animator.Play("Jump", 0, 0f);
            }
        }
    }

    void HandleSlide()
    {
        if (Input.GetKey(KeyCode.C) && isGround)
        {
            isSliding = true;
            RunnCol.enabled = false;
            SlcCol.enabled = true;
            ChangeAnim(State.Slide);
        }
        else
        {
            isSliding = false;
            RunnCol.enabled = true;
            SlcCol.enabled = false;
            //if (isGround && !isJumping)
            //    ChangeAnim(State.Run);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            Debug.Log("점프 초기화");
            isGround = true;
            jumpCount = 0;
            isJumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
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

    // 💡 RunToRightEdge 코루틴 추가
    IEnumerator RunToRightEdge()
    {
        isRunningToEdge = true;
        spriteRenderer.flipX = false; // 달릴 때 방향을 오른쪽으로 설정

        float screenRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(0.75f, 0, 0)).x;

        while (transform.position.x < screenRightEdge)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);  // maxSpeed로 달리기 속도 설정
            yield return null;
        }

        rigid.velocity = Vector2.zero; // 달리기 멈추기
        yield return new WaitForSeconds(0.5f); // 잠시 대기

        spriteRenderer.flipX = true;  // 방향을 왼쪽으로 반전
        isRunningToEdge = false;
    }
}