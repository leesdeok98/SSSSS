using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public static SpeedManager Instance;

    public float moveSpeed = 30f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}