using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager2 : MonoBehaviour
{
    public Slider SFXSlider;

    [SerializeField] private GameObject pausePanel;

    [SerializeField] private Button titleButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button overTitleButton;
    [SerializeField] private Button overRetryButton;

    void Start()
    {
        if (titleButton == null)
        {
            titleButton = GetComponent<Button>();
        }

        if (titleButton != null)
        {
            titleButton.onClick.AddListener(OnTitleButtonClicked);
        }

        if(retryButton == null)
        {
            retryButton = GetComponent<Button>();
        }
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(OnRetryButtonClicked);
        }

        if (backButton == null)
        {
            backButton = GetComponent<Button>();
        }
        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
        }
        if (overTitleButton == null)
        {
            overTitleButton = GetComponent<Button>();
        }

        if (overTitleButton != null)
        {
            overTitleButton.onClick.AddListener(OnTitleButtonClicked);
        }

        if (overRetryButton == null)
        {
            overRetryButton = GetComponent<Button>();
        }
        if (overRetryButton != null)
        {
            overRetryButton.onClick.AddListener(OnRetryButtonClicked);
        }

    }

    void OnTitleButtonClicked()
    {
        SoundManager2.Instance.Play("PB");
        LoadNextScene();
    }

    void OnRetryButtonClicked()
    {
        SoundManager2.Instance.Play("PB");
        LoadNextScene2();
    }
    void OnBackButtonClicked()
    {
        SoundManager2.Instance.Play("PB");
        pausePanel.SetActive(false);
    }

    void LoadNextScene()
    {
        Debug.Log("Main Scene Load");
        SceneManager.LoadScene("Main");
    }

    void LoadNextScene2()
    {
        Debug.Log("Retry Scene Load");
        SceneManager.LoadScene("GameView");
    }


}
