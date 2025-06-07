
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChapterType
{
    Chapter1 = 1,
    Chapter2 = 2,
    Chapter3 = 3,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverUI;

    private int collectedCoins = 0;
    private int requiredCoins = 0;
    public int currentChapter = (int)ChapterType.Chapter1;

    public int GetCurrentChapter() { return currentChapter; }
    public int GetCollectedCoins() { return collectedCoins; }
    public int GetRequiredCoins() { return requiredCoins; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddCoin()
    {
        collectedCoins++;
        UpdateCoinUI();
    }

    public void ResetCoins()
    {
        collectedCoins = 0;
        UpdateCoinUI();
    }

    public void SetRequiredCoins()
    {
        requiredCoins = currentChapter;
        UpdateCoinUI();
    }

    public bool HasEnoughCoins() { return collectedCoins >= requiredCoins; }

    private void UpdateCoinUI()
    {
        UIManager.instance?.UpdateCoinUI(collectedCoins, requiredCoins);
    }
}
