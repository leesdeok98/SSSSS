using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Shield")]
    public GameObject shieldObject;
    public float shieldDuration = 2f;
    public bool isShieldActive = false;

    [Header("Movement")]
    public float JumpForce;
    public Rigidbody2D rb;
    public Animator PlayerAnimator;
    public BoxCollider2D SlcCol;
    public BoxCollider2D RunnCol;

    private int maxLives = 3;
    private int currentLives;
    public int dreamEnergyCount = 0;


    private float invincibleTime = 1.5f;
    private float hurtDuration = 0.3f;

    private bool isHurt = false;
    private bool isInvincible = false;

    private bool isGround = false;
    private int jumpCount = 0;
    private bool isJumping = false;
    public int jumpLevel = 2;

    private SpriteRenderer spr;
    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        rb = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

        SlcCol.enabled = false;
        RunnCol.enabled = true;
    }

    private void Start()
    {
        currentLives = maxLives;
        UIManager.instance.UpdateLivesUI(currentLives);
        UIManager.instance.UpdateDreamEnergyUI(dreamEnergyCount);
        SetGroundTrue();
        shieldObject.SetActive(false);
    }

    private void Update()
    {
        Slide();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("TryJump", 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ActivateShield();
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
        if (collider.CompareTag("Enemy") || collider.CompareTag("Lightning"))
        {
            if (isShieldActive)
            {
                Destroy(collider.gameObject);
            }
            else if (!isInvincible)
            {
                TakeDamage();
                Destroy(collider.gameObject);
            }
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
        Debug.Log("Game Over");
        // TODO: 게임 오버 처리
    }

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

    public void ActivateShield()
    {
        if (isShieldActive || dreamEnergyCount < 2) return;

        dreamEnergyCount -= 2;
        UIManager.instance.UpdateDreamEnergyUI(dreamEnergyCount);

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

    public void AddDreamEnergy()
    {
        dreamEnergyCount++;
        UIManager.instance.UpdateDreamEnergyUI(dreamEnergyCount);

        // 조건을 명확히: 2개 이상이면 쉴드 발동
        if (dreamEnergyCount >= 2 && !isShieldActive)
        {
            ActivateShield();
        }
    }

    
}
