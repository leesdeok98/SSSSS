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

    public BoxCollider2D boxCol; // 🔄 단일 콜라이더

    private Vector2 normalSize = new Vector2(0.4f, 1.7f);
    private Vector2 normalOffset = new Vector2(0.1f, 0.8f);

    private Vector2 slideSize = new Vector2(2.0f, 0.7f);   // ← 이미지 참고
    private Vector2 slideOffset = new Vector2(0.0f, 0.4f);

    private int maxLives = 3;
    private int currentLives;

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
    private bool isControlLocked = false;

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

    public float destroyX = 11f;
    public System.Action OnReachedDestroyX;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        rb = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        boxCol = GetComponent<BoxCollider2D>();
        boxCol.size = normalSize;
        boxCol.offset = normalOffset;
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
        currentLives = maxLives;
        UIManager.instance.UpdateLivesUI(currentLives);
        UIManager.instance.UpdateCoinUI(0, 0);
        isGround = true;

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
        if (isControlLocked) return;

        Slide();

        if (Input.GetKeyDown(KeyCode.S))
        {
            SoundManager.Instance.Play("Sliding");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (playerdie) return;
            Invoke("TryJump", 0.08f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            PlayerAnimator.SetInteger("State", 0);
            isJumping = false;
            jumpCount = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("RunTrigger"))
        {
            if (!isRunningToEdge)
            {
                StartCoroutine(RunFixedDistance());
            }
        }
        if (collider.CompareTag(targetTag))
        {
            Invoke("ShowBoss", 2f);
        }
        if (collider.CompareTag("Chapter"))
        {
            LightOutEffect effect = FindObjectOfType<LightOutEffect>();
            if (effect != null)
            {
                effect.StartCoroutine("FlashLoop");
            }
        }

        if (collider.CompareTag("Stop"))
        {
            Debug.Log("🚫 Stop Trigger Detected! Controls locked for 3 seconds.");

            PlayerAnimator.SetInteger("State", 0);
            boxCol.size = normalSize;
            boxCol.offset = normalOffset;

            StartCoroutine(LockControlsForSeconds(3f));
        }

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
        if (isRunningToEdge) yield break;

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

        currentLives--;
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
        isControlLocked = true;
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
        if (isHurt || isControlLocked) return;

        if (isGround)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            PlayerAnimator.SetInteger("State", 1);
            SoundManager.Instance.Play("Jump");
            isGround = false;
            isJumping = true;
            jumpCount++;

            // 🔒 무조건 일반 콜라이더 적용
            boxCol.size = normalSize;
            boxCol.offset = normalOffset;
        }
        else if (isJumping && jumpCount < jumpLevel)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * 38f, ForceMode2D.Impulse);
            PlayerAnimator.Play("Jump", 0, 0f);
            SoundManager.Instance.Play("Jump");
            isJumping = false;

            // 🔒 무조건 일반 콜라이더 적용
            boxCol.size = normalSize;
            boxCol.offset = normalOffset;
        }
    }

    void Slide()
    {
        if (isHurt || isControlLocked) return;

        if (Input.GetKey(KeyCode.S) && isGround)
        {
            PlayerAnimator.SetInteger("State", 2);
            isGround = true;
            isJumping = false;

            // 🔒 슬라이딩 콜라이더 강제 적용
            boxCol.size = slideSize;
            boxCol.offset = slideOffset;
        }
        else if (isGround)
        {
            PlayerAnimator.SetInteger("State", 0);

            // 🔒 일반 콜라이더 강제 적용
            boxCol.size = normalSize;
            boxCol.offset = normalOffset;
        }
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
        UIManager.instance.HideCoinUI();
        UIManager.instance.HideLivesUI();
    }

    void ShowBoss()
    {
        if (Boss != null)
            Boss.SetActive(true);
    }

    public void StartAutoRun()
    {
        isControlLocked = true; // 입력 막기
        StartCoroutine(AutoRunRoutine());
    }

    private IEnumerator AutoRunRoutine()
    {
        PlayerAnimator.SetInteger("State", 0);
        float speed = 5f;
        rb.gravityScale = 0f;

        while (transform.position.x < destroyX)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            yield return null;
        }

        rb.velocity = Vector2.zero;

        OnReachedDestroyX?.Invoke();
        Destroy(gameObject);
    }
    public bool IsDead()
    {
        return playerdie;
    }

}