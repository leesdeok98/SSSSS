using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image[] heartImages;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateLivesUI(heartImages.Length);
        UpdateCoinUI(0);
    }

    public void UpdateLivesUI(int lives)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].gameObject.SetActive(i < lives);
        }
    }

    public void UpdateCoinUI(int coinCount)
    {
        if (coinText != null)
        {
            coinText.text = "¡¿ {coinCount}";
        }
    }
}