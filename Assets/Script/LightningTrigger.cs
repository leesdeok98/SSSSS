using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTrigger : MonoBehaviour
{
    public int requiredCoins = 0;
    public int nextChapter = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"���� �浹: {other.gameObject.name}");

            if (GameManager.instance == null || LightningManager.instance == null)
            {
                Debug.LogWarning("GameManager or LightningManager missing");
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

            if (hasEnoughCoins)
            {
                GameManager.instance.GoToChapter(nextChapter);
                Debug.Log("���� ���! ���� é�ͷ� �̵�.");
            }
            else
            {
                Debug.Log("���� ����! �÷��̾� ���.");
            }
        }
    }
}