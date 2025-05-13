using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{    
    private float offset = 20.6f;
    private float resentPosX = -19.3f;
    public float speed;
    public SpriteRenderer[] backgrounds;
    private int firstIndex = 1;
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
