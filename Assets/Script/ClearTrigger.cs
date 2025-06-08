using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearTrigger : MonoBehaviour
{
    public string nextSceneName = "Clear";
    private bool triggered = false;  // 중복 실행 방지

    public ClearFadeEffect clearFade;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (other.CompareTag("Player"))
        {
            triggered = true;
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.StartAutoRun();
                StartCoroutine(ClearSequence());
            }
        }
    }

    private IEnumerator ClearSequence()
    {
        yield return new WaitForSeconds(0f);    // 플레이어 달리기 시작한 n초후 (필요없으면 그냥 줄 삭제)
        yield return clearFade.StartFadeOut();
        Invoke("clear", 1f);
    }
    void clear()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}