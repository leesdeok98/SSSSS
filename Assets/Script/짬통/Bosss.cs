using UnityEngine;
using System.Collections;

public class Bosss : MonoBehaviour
{
    public static Bosss instance;

    public GameObject Boss;
    public Animator anim;

    private int currentState = 2; // ���� ���۰�

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
        // ���°� 9 �̻��̸� �̹� �� ���� �����̹Ƿ� �� �̻� ���� �� ��
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
        yield return new WaitForSeconds(1f);

        anim.SetInteger("State", currentState + 1);
        yield return new WaitForSeconds(2.5f);

        anim.SetInteger("State", currentState + 2);
        yield return new WaitForSeconds(2.8f);

        anim.SetInteger("State", currentState + 3);

        // currentState�� 6�� ��� �� 6,7,8,9���� ���������Ƿ� �ı�
        if (currentState == 6)
        {
            yield return new WaitForSeconds(0.5f); // ������ �ִϸ��̼� ��� ��ٸ�
            Destroy(gameObject);
        }

        currentState += 4; // ���� Damage �ܰ�� �Ѿ
    }
}
