using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerMoveing : MonoBehaviour
{
    public static PlayerMoveing instance;

    private int maxLives = 3;
    private int currentLives;

    private float invincibleTime = 1.5f;
    private bool isHurt;

    private int coinCount = 0;

    [Header("Movement")]
    public float JumpForce;
    public Rigidbody2D rb;
    public Animator PlayerAnimator;
    public BoxCollider2D SlcCol;
    public BoxCollider2D RunnCol;

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
        SetGroundTrue();
    }

    private void Update()
    {
        if (isHurt) return;

        Slide();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("TryJump", 0.1f);
        }

        // ½¯µå °ü·Ã ÄÚµå Á¦°ÅµÊ
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

    public void RestoreFullHP()
    {
        currentLives = maxLives;
        UIManager.instance.UpdateLivesUI(currentLives);
    }

    public void AddCoin()
    {
        coinCount++;
        // UIManager.instance.UpdateDreamEnergyUI(coinCount);
    }
}
