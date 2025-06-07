using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private GameObject gameOverPanel;

    [Header("Movement")]
    public float JumpForce;
    public Rigidbody2D rb;
    public Animator PlayerAnimator;
    public BoxCollider2D SlcCol;
    public BoxCollider2D RunnCol;

    private int maxLives = 3;
    private int currentLives;
    public int CoinCount = 0;

    private float invincibleTime = 1.5f;
    private float hurtDuration = 0.3f;

    private bool isHurt = false;
    private bool isInvincible = false;
    private bool playerdie = false;

    private bool isSliding = false;
    private bool isGround = false;
    private int jumpCount = 0;
    private bool isJumping = false;
    public int jumpLevel = 2;
    private bool isPaused = false;

    private bool isControlLocked = false; // ✅ 입력 잠금 변수 추가

    public GameObject Boss;
    public string targetTag = "Chapter";


    private SpriteRenderer spr;
    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    [Header("Auto Run Settings")]
    public float runDistance = 8f;
    public float runToEdgeSpeed = 5f;
    private bool isRunningToEdge = false;
    private bool isFeathering = false;

    public enum Direction { Right, Left }
    public Direction currentDirection = Direction.Right;


    private Rigidbody2D rigid;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        rb = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        SlcCol.enabled = false;
        RunnCol.enabled = true;
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
        currentLives = maxLives;
        UIManager.instance.UpdateLivesUI(currentLives);
        UIManager.instance.UpdateCoinUI(0, 0);
        SetGroundTrue();

        // 시작 시 배경 방향도 확실히 맞춰주기
        SetBackgroundDirectionAccordingToPlayer();
    }
    void SetBackgroundDirectionAccordingToPlayer()
    {
        int scrollerDir = (currentDirection == Direction.Right) ? 1 : -1;

        foreach (var scroller in FindObjectsOfType<Scorller>())
        {
            scroller.SetDirection(scrollerDir);
        }

        if (BackgroundScrolling.Instance != null)
        {
            BackgroundScrolling.Instance.SetDirection(scrollerDir);
        }
    }

    private void Update()
    {
        if (isControlLocked) return; // ✅ 컨트롤 잠금 시 입력 무시

        Slide();
        if (Input.GetKeyDown(KeyCode.S))
        {
            SoundManager.Instance.Play("Sliding");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.Play("Jump");
            if (playerdie) return;
            Invoke("TryJump", 0.08f);
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SetGroundTrue();
            PlayerAnimator.SetInteger("State", 0);
            isJumping = false;
            jumpCount = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("RunTrigger"))
        {
            if (!isRunningToEdge)  // 중복 실행 방지
            {
                StartCoroutine(RunFixedDistance());
            }
        }
        if (collider.CompareTag(targetTag))
        {
            Invoke("ShowBoss", 2f); // 2초 뒤에 보스 등장
        }
        if (collider.CompareTag("Chapter"))
        {
            // LightOutEffect 스크립트 찾고 FlashLoop 실행
            LightOutEffect effect = FindObjectOfType<LightOutEffect>();
            if (effect != null)
            {
                effect.StartCoroutine("FlashLoop");
            }
        }


        // Stop 트리거도 여기서 처리
        if (collider.CompareTag("Stop"))
        {
            Debug.Log("🚫 Stop Trigger Detected! Controls locked for 3 seconds.");

            // 슬라이딩 강제 해제
            PlayerAnimator.SetInteger("State", 0);
            SlcCol.enabled = false;
            RunnCol.enabled = true;

            StartCoroutine(LockControlsForSeconds(3f));
        }

        // 적 또는 번개 충돌 처리
        if (collider.CompareTag("Enemy"))
        {
            if (!isInvincible)
            {
                TakeDamage();
                Destroy(collider.gameObject);
            }
        }
    }


    IEnumerator RunFixedDistance()
    {
        if (isRunningToEdge)
            yield break; // 이미 실행 중이면 종료

        isRunningToEdge = true;
        PlayerAnimator.SetInteger("State", 0);

        float startX = transform.position.x;
        float targetX = (currentDirection == Direction.Right) ? startX + runDistance : startX - runDistance;

        spr.flipX = (currentDirection == Direction.Left);

        while ((currentDirection == Direction.Right && transform.position.x < targetX) ||
               (currentDirection == Direction.Left && transform.position.x > targetX))
        {
            rigid.velocity = new Vector2(
                (currentDirection == Direction.Right ? runToEdgeSpeed : -runToEdgeSpeed),
                rigid.velocity.y
            );
            yield return null;
        }

        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);

        FlipDirection();

        isRunningToEdge = false;

        // 디버그 추가
        Debug.Log("RunFixedDistance 완료, 현재 방향: " + currentDirection);

        // 필요 시 추가 조작
        GetComponent<SpriteRenderer>().flipX = (currentDirection == Direction.Left);
    }


    void FlipDirection()
    {
        currentDirection = (currentDirection == Direction.Right) ? Direction.Left : Direction.Right;
        spr.flipX = (currentDirection == Direction.Left);

        int scrollerDir = (currentDirection == Direction.Right) ? 1 : -1;

        foreach (var scroller in FindObjectsOfType<Scorller>())
        {
            scroller.SetDirection(scrollerDir);
        }

        if (BackgroundScrolling.Instance != null)
        {
            BackgroundScrolling.Instance.SetDirection(scrollerDir);
        }
    }


    public void TakeDamage()
    {
        if (isInvincible) return;

        currentLives--;   //데미지 까이는 코드 실행
        UIManager.instance.UpdateLivesUI(currentLives);

        if (currentLives <= 0)
        {
            Die();
        }
        else
        {
            isHurt = true;
            isInvincible = true;
            PlayerAnimator.SetInteger("State", 3);

            Invoke("EndHurtAnimation", hurtDuration);
            Invoke("RecoverFromDamage", invincibleTime);

            StartCoroutine(AlphaBlink());

            StopManager stopManager = FindObjectOfType<StopManager>();
            if (stopManager != null)
            {
                stopManager.speed = 0f;
            }
        }
    }

    void EndHurtAnimation()
    {
        isHurt = false;
        PlayerAnimator.SetInteger("State", 0);
    }

    void RecoverFromDamage()
    {
        isInvincible = false;
        spr.color = fullA;
    }

    IEnumerator AlphaBlink()
    {
        while (isInvincible)
        {
            spr.color = halfA;
            yield return new WaitForSeconds(0.1f);
            spr.color = fullA;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Die()
    {
        SoundManager.Instance.Play("Dead");
        playerdie = true;
        isControlLocked = true; // 🔧 이거 추가!
        Debug.Log("Game Over");
        PlayerAnimator.SetInteger("State", 4);
        SpeedManager.Instance.moveSpeed = 0f;
        BackgroundScrolling.Instance.speed = 0f;

        StopManager stopManager = FindObjectOfType<StopManager>();
        if (stopManager != null)
        {
            stopManager.speed = 0f;
        }
        Invoke("Gameover", 2f);
    }

    public void ForceDie()
    {
        currentLives = 0;
        UIManager.instance.UpdateLivesUI(0);
        Die();
    }


    void TryJump()
    {
        if (isHurt || isControlLocked) return; // ✅ 잠금 확인 추가

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
            rb.AddForce(Vector2.up * 38f, ForceMode2D.Impulse);
            PlayerAnimator.Play("Jump", 0, 0f);
            isJumping = false;
        }
    }

    void Slide()
    {
        if (isHurt || isControlLocked) return; // ✅ 잠금 확인 추가

        if (Input.GetKey(KeyCode.S) && isGround)
        {
            PlayerAnimator.SetInteger("State", 2);
            SlcCol.enabled = true;
            RunnCol.enabled = false;
        }
        else if (isGround)
        {
            PlayerAnimator.SetInteger("State", 0);
            SlcCol.enabled = false;
            RunnCol.enabled = true;
        }
    }

    void SetGroundTrue()
    {
        isGround = true;
    }

    public void RestoreFullHP()
    {
        currentLives = maxLives;
        UIManager.instance.UpdateLivesUI(currentLives);
    }

    private IEnumerator LockControlsForSeconds(float seconds)
    {
        isControlLocked = true;
        yield return new WaitForSeconds(seconds);
        isControlLocked = false;
    }
    void Gameover()
    {
        gameOverPanel.SetActive(true);
    }

    void ShowBoss()
    {
        if (Boss != null)
            Boss.SetActive(true);
    }
}