
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
    public static LightningManager instance;

    public GameObject lightning1;
    public GameObject lightning2;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void TriggerLightning(bool hasEnoughCoins)
    {
        if (hasEnoughCoins)
        {
            if (lightning2 != null)
                lightning2.SetActive(true);
        }
        else
        {
            if (lightning1 != null)
                lightning1.SetActive(true);

            if (PlayerController.instance != null)
                PlayerController.instance.Invoke("ForceDie", 0.3f);
        }
    }
}
