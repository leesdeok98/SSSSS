using UnityEngine;

public class Bosss : MonoBehaviour
{
    public static Bosss instance;
    public GameObject Boss;
    public Animator anim;

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
        anim.SetInteger("State", 1);
    }
    public void BossIdle()
    {
        anim.SetInteger("State", 1);
    }
}
