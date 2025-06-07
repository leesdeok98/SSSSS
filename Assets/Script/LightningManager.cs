
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
    public static LightningManager instance;

    public GameObject lightning1; //�ɺ� �״� ����
    public GameObject lightning2; //�ǵ� ����
    public GameObject Bosslightning; //�������� �������� ����


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void ActivateBossLightning()
    {
        if (Bosslightning != null)
            Bosslightning.SetActive(true);
    }

    void ShildeLigtning()
    {
        //������ ����� ��� 
        if (lightning2 != null)
            lightning2.SetActive(true);//�ǵ����
    }

    public void TriggerLightning(bool hasEnoughCoins)
    {
        if (hasEnoughCoins)
        {
            if (Bosslightning != null)
                Invoke("ShildeLigtning", 0.5f);

            if (Bosslightning != null)
                Invoke("ActivateBossLightning", 0.8f);
                
            

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
