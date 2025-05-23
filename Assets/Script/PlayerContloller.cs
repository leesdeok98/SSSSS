using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static PlayerController instance;

    // ======== Shield ���� ========
    [Header("Shield")]
    public GameObject shieldObject;
    public float shieldDuration = 2f;
    public bool isShieldActive = false;

    // ======== �̵� ���� ������Ʈ ========
    [Header("Movement")]
    public float JumpForce;
    public Rigidbody2D rb;
    public Animator PlayerAnimator;
    public BoxCollider2D SlcCol;    // �����̵� �ݶ��̴�
    public BoxCollider2D RunnCol;   // �޸��� �ݶ��̴�

    // ======== �÷��̾� ���� ========
    private int maxLives = 3;
    private int currentLives;
    private int coinCount = 0;

    private float invincibleTime = 1.5f;
    private bool isHurt;

    // ======== ���� ���� ���� ========
    private bool isGround = false;
    private int jumpCount = 0;
    private bool isJumping = false;
    public int jumpLevel = 2;

    // ======== ��Ÿ ������Ʈ ========
    private SpriteRenderer spr;
    Color halfA = new Color(1, 1, 1, 0.5f);  // ���� ������ ȿ��
    Color fullA = new Color(1, 1, 1, 1);

    // ========== Unity �޼��� ==========

    private void Awake()
    {
        // �̱��� ����
        if (instance == null) instance = this;
        else Destroy(gameObject);

        // ������Ʈ ���� ĳ��
        rb = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

        // �⺻ �ݶ��̴� ����
        SlcCol.enabled = false;
        RunnCol.enabled = true;
    }

    private void Start()
    {
        // �ʱ� ü�� �� ���� UI ����
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
            // �Է� ���� ����
            Invoke("TryJump", 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ActivateShield();
        }
    }

    // ========== �浹 ó�� ==========

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
                Destroy(collider.gameObject); // ���� ���¸� �� ����
            }
            else if (!isHurt)
            {
                TakeDamage();
                Destroy(collider.gameObject);
            }
        }
    }

    // ========== ������ �� ���� ó�� ==========

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
        // TODO: ���� ���� ó�� �߰�
    }

    // ========== ����/�����̵� ==========

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

    // ========== ���� �� ��Ÿ ��ƿ ==========

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
