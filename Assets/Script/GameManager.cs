
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
        switch ((ChapterType)currentChapter)
        {
            case ChapterType.Chapter1:
                requiredCoins = 0;
                break;
            case ChapterType.Chapter2:
                requiredCoins = 2;
                break;
            case ChapterType.Chapter3:
                requiredCoins = 3;
                break;
        }
        UpdateCoinUI();
    }

    public bool HasEnoughCoins() { return collectedCoins >= requiredCoins; }

    public void GoToChapter(int chapter)
    {
        currentChapter = chapter;
        ResetCoins();
        SetRequiredCoins();
    }

    private void UpdateCoinUI()
    {
        UIManager.instance?.UpdateCoinUI(collectedCoins, requiredCoins);
    }
}
