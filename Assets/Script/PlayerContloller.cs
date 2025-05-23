using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static PlayerController instance;

    // ======== Shield 관련 ========
    [Header("Shield")]
    public GameObject shieldObject;
    public float shieldDuration = 2f;
    public bool isShieldActive = false;

    // ======== 이동 관련 컴포넌트 ========
    [Header("Movement")]
    public float JumpForce;
    public Rigidbody2D rb;
    public Animator PlayerAnimator;
    public BoxCollider2D SlcCol;    // 슬라이딩 콜라이더
    public BoxCollider2D RunnCol;   // 달리기 콜라이더

    // ======== 플레이어 상태 ========
    private int maxLives = 3;
    private int currentLives;
    private int coinCount = 0;

    private float invincibleTime = 1.5f;
    private bool isHurt;

    // ======== 점프 관련 상태 ========
    private bool isGround = false;
    private int jumpCount = 0;
    private bool isJumping = false;
    public int jumpLevel = 2;

    // ======== 기타 컴포넌트 ========
    private SpriteRenderer spr;
    Color halfA = new Color(1, 1, 1, 0.5f);  // 투명도 깜빡임 효과
    Color fullA = new Color(1, 1, 1, 1);

    // ========== Unity 메서드 ==========

    private void Awake()
    {
        // 싱글톤 설정
        if (instance == null) instance = this;
        else Destroy(gameObject);

        // 컴포넌트 참조 캐싱
        rb = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

        // 기본 콜라이더 설정
        SlcCol.enabled = false;
        RunnCol.enabled = true;
    }

    private void Start()
    {
        // 초기 체력 및 코인 UI 설정
        currentLives = maxLives;
        UIManager.instance.UpdateLivesUI(currentLives);
        UIManager.instance.UpdateCoinUI(coinCount);
        SetGroundTrue();
    }

    private void Update()
    {
        if (isHurt) return;

        Slide();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 입력 지연 보정
            Invoke("TryJump", 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ActivateShield();
        }
    }

    // ========== 충돌 처리 ==========

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
        if (collider.CompareTag("Enemy") || collider.CompareTag("Lightning"))
        {
            if (isShieldActive)
            {
                Destroy(collider.gameObject); // 쉴드 상태면 적 제거
            }
            else if (!isHurt)
            {
                TakeDamage();
                Destroy(collider.gameObject);
            }
        }
    }

    // ========== 데미지 및 죽음 처리 ==========

    public void TakeDamage()
    {
        if (isHurt) return;

        currentLives--;
        UIManager.instance.UpdateLivesUI(currentLives);

        if (currentLives <= 0)
        {
            Die();
        }
        else
        {
            isHurt = true;
            PlayerAnimator.SetInteger("State", 3);
            rb.velocity = Vector2.zero;
            StartCoroutine(AlphaBlink());
            Invoke("RecoverFromDamage", invincibleTime);
        }
    }

    void RecoverFromDamage()
    {
        isHurt = false;
        PlayerAnimator.SetInteger("State", 0);
        spr.color = fullA;
    }

    IEnumerator AlphaBlink()
    {
        while (isHurt)
        {
            spr.color = halfA;
            yield return new WaitForSeconds(0.1f);
            spr.color = fullA;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Die()
    {
        Debug.Log("Game Over");
        // TODO: 게임 오버 처리 추가
    }

    // ========== 점프/슬라이드 ==========

    void TryJump()
    {
        if (isHurt) return;

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
            PlayerAnimator.Play("Jump", 0, 0f);
            isJumping = false;
        }
    }

    void Slide()
    {
        if (isHurt) return;

        if (Input.GetKey(KeyCode.C) && isGround)
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

    // ========== 쉴드 및 기타 유틸 ==========

    public void ActivateShield()
    {
        if (isShieldActive || coinCount < 1) return;

        coinCount--;
        UIManager.instance.UpdateCoinUI(coinCount);

        isShieldActive = true;
        StartCoroutine(ShieldRoutine());
    }

    private IEnumerator ShieldRoutine()
    {
        shieldObject.SetActive(true);
        yield return new WaitForSeconds(shieldDuration);
        shieldObject.SetActive(false);
        isShieldActive = false;
    }

    public void RestoreFullHP()
    {
        currentLives = maxLives;
        UIManager.instance.UpdateLivesUI(currentLives);
    }

    public void AddCoin()
    {
        coinCount++;
        UIManager.instance.UpdateCoinUI(coinCount);
    }
}
