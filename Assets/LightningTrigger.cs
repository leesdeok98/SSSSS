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
            Debug.Log("���� ���! ���� é�ͷ� �̵�.");
        }
        else
        {
            Debug.Log("���� ����! �÷��̾� ���.");
        }
    }
}