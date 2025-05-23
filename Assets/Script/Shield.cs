using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shield : MonoBehaviour
    
{
    //[SerializeField] private GameObject shieldObject; // 쉴드 오브젝트
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lightning"))
        {
            Destroy(other.gameObject); 
            PlayerController.instance.isShieldActive = false; 
            gameObject.SetActive(false); // Deactivate the shield object
        }
    }
}
