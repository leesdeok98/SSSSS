using UnityEngine;
using System.Collections;

public class Bosss : MonoBehaviour
{
    public static Bosss instance;

    public GameObject Boss;
    public Animator anim;

    private int currentState = 2; // 상태 시작값

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        if (Boss != null)
            Boss.SetActive(false);
        else
            Debug.LogWarning("Boss object is not assigned in the Inspector!");

        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Invoke("BossIdle", 3f);
    }

    public void BossSpawn()
    {
        if (Boss != null)
            Boss.SetActive(true);

        anim.SetInteger("State", 1); // Idle
        SoundManager.Instance.Play("BAp");
    }

    public void BossIdle()
    {
        anim.SetInteger("State", 1);
    }

    public void BossTakeDamege()
    {
        if (currentState > 8) return;

        StartCoroutine(BossDamageSequence());
    }

    public void BossWin()
    {
        anim.SetInteger("State", 9);
    }

    private IEnumerator BossDamageSequence()
    {
        anim.SetInteger("State", currentState);
        yield return new WaitForSeconds(1.6f);

        anim.SetInteger("State", currentState + 1);
        yield return new WaitForSeconds(2.5f);

        anim.SetInteger("State", currentState + 2);
        yield return new WaitForSeconds(2.2f);

        anim.SetInteger("State", currentState + 3);

        if (currentState == 6)
        {
            yield return new WaitForSeconds(0.5f); // 마지막 애니메이션 잠깐 기다림
            Destroy(gameObject);
        }

        currentState += 4;
    }

    private void Update()
    {
        if (PlayerController.instance != null && PlayerController.instance.IsDead())
        {
            StartCoroutine(StopBossAnimationAfterDelay());
        }
    }

    private bool isStopped = false;

    private IEnumerator StopBossAnimationAfterDelay()
    {
        if (isStopped) yield break; // 중복 호출 방지

        isStopped = true;
        yield return new WaitForSeconds(0.2f);

        if (anim != null)
        {
            anim.enabled = false;
            Debug.Log("🛑 Boss 애니메이터 완전히 중단됨");
        }
    }
}
