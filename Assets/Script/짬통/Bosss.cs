using UnityEngine;

public class Bosss : MonoBehaviour
{
    public static Bosss instance;
    public GameObject Boss;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        if (Boss != null)
            Boss.SetActive(false);
        else
            Debug.LogWarning("Boss object is not assigned in the Inspector!");
    }

    public void BossSpawn()
    {
        if (Boss != null)
            Boss.SetActive(true);
    }
}
