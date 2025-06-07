using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewChapter : MonoBehaviour
{
    public int nextChapter = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Bosss.instance.Invoke("BossSpawn", 1f);
        }
    }
}
