using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrigger : MonoBehaviour
{
    public int requiredCoins = 0;
    public int nextChapter = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.instance == null || LightningManager.instance == null)
        {
            Debug.LogWarning("GameManager or LightningManager missing");
            return;
        }

        bool hasEnoughCoins = GameManager.instance.HasEnoughCoins();

        LightningManager.instance.TriggerLightning(hasEnoughCoins);

        if (hasEnoughCoins)
        {
            Debug.Log("코인 충분! 다음 챕터로 이동.");
        }
        else
        {
            Debug.Log("코인 부족! 플레이어 사망.");
        }
    }
}