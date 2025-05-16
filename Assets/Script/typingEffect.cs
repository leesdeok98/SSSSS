using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class typingEffect : MonoBehaviour
{
    public TMP_Text tx;
    private string m_text = "";
    // Start is called before the first frame update
    void Start()
    {
        tx.gameObject.SetActive(false);
        StartCoroutine(_typing());
    }
    IEnumerator _typing()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i <= m_text.Length; i++)
        {
            tx.text = m_text.Substring(0, i);
            tx.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.15f);
        }
    }
} 