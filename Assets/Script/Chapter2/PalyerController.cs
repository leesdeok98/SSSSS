using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PalyerController : MonoBehaviour
{
    public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(0,speed,0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(0,-speed,0);
        }
    }
}
