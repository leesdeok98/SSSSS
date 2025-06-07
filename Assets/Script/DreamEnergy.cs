using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamEnergy : MonoBehaviour
{
    public static DreamEnergy instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddCoin();
            // AudioManager.instance.PlaySFX(AudioManager.SFX.DE); //  SFX Àç»ý
            Destroy(gameObject);
        }
    }
}