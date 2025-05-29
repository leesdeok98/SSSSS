using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public static SpeedManager Instance;

    public float moveSpeed = 10f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        SpeedManager.Instance.moveSpeed = 10f;
    }
}
