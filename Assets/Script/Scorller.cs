using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorller : MonoBehaviour
{
    public int count;
    public float speedRate = 1f;

    public static int globalDirection = 1;  
    private int direction = 1;

    void Start()
    {
        count = transform.childCount;
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * -1f * speedRate * direction, 0, 0);
    }

    public void SetDirection(int newDirection)
    {
        direction = newDirection;
        globalDirection = newDirection;  
    }
}