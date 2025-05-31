using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{    
    private float offset = 24f;
    private float resentPosX = -23.5f;
    public float speed = 10;
    public SpriteRenderer[] backgrounds;
    public static BackgroundScrolling Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // 중복 방지
    }
    void Update()
    {
        for(int i = 0;i<backgrounds.Length;i++)
        {
            backgrounds[i].transform.position += Vector3.left * Time.deltaTime * speed;
            if(backgrounds[i].transform.position.x <= resentPosX)
            {
                Vector3 newPos = backgrounds[i].transform.position;
                newPos.x = backgrounds[1-i].transform.position.x + offset;
                backgrounds[i].transform.position =  newPos;
            }
        }
    }
}
