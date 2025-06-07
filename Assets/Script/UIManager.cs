
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image[] heartImages;
    public Sprite redHeart;
    public Sprite blackHeart;

    public Image coinIcon;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateLivesUI(heartImages.Length);
        UpdateCoinUI(0, 0);
    }

    public void UpdateLivesUI(int lives)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].gameObject.SetActive(true);

            if (i < lives)
            {
                heartImages[i].sprite = redHeart;
            }
            else
            {
                heartImages[i].sprite = blackHeart;
                SoundManager.Instance.Play("Hit");
            }
        }
    }

    public void UpdateCoinUI(int collected, int required)
    {
        bool show = required > 0;
        coinIcon.gameObject.SetActive(show);
        coinText.gameObject.SetActive(show);

        if (show)
            coinText.text = $"{collected} / {required}";
    }

    public void HideCoinUI()
    {
        coinIcon.gameObject.SetActive(false);
        coinText.gameObject.SetActive(false);
    }

    public void HideLivesUI()
    {
        foreach (Image heart in heartImages)
        {
            heart.gameObject.SetActive(false);
        }
    }
}
