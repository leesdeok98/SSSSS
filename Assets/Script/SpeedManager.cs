using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public static SpeedManager Instance;

    public float moveSpeed = 10f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // 중복 방지
    }

    private void Start()
    {
        // 시작 속도 설정 — 나중에 변경 가능
        moveSpeed = 10f;
    }
}
