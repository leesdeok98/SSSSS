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
                player.OnReachedDestroyX += () => StartCoroutine(ClearSequence());
                player.StartAutoRun();
            }
        }
    }

    private IEnumerator ClearSequence()
    {
        yield return clearFade.StartFadeOut();
        SceneManager.LoadScene(nextSceneName);
    }
}