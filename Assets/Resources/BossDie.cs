using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("BossDead", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BossDead();
        }
    }
    void BossDead()
    {
        SoundManager.Instance.Play("BD");
    }
}
