using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrigger : MonoBehaviour
{
    public int requiredCoins = 0;
    //public int nextChapter = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance == null || LightningManager.instance == null)
            {
                return;
            }
            if (Bosss.instance != null)
            {
                Bosss.instance.BossTakeDamege();
            }

            bool hasEnoughCoins = GameManager.instance.HasEnoughCoins();
            int required = GameManager.instance.GetRequiredCoins();
            int collected = GameManager.instance.GetCollectedCoins();

            LightningManager.instance.TriggerLightning(hasEnoughCoins);

            
        }
    }
}