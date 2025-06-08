using System.Collections;
using UnityEngine;

public class ChapterTrigger : MonoBehaviour
{
    private bool isChangeTime = false;
    public int nextChapter = 2;

    private void Start()
    {
        isChangeTime = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChapterManager manager = FindObjectOfType<ChapterManager>();

            if (manager != null && !isChangeTime)
            {
                if (nextChapter == 4)
                {
                    UIManager.instance?.HideLivesUI();
                    StartCoroutine(DelayedGoToNextChapter());
                    return;
                }

                isChangeTime = true;
                manager.TryChangeChapter();
                StartCoroutine(DelayedGoToNextChapter());
                Invoke("ChangeBool", 10.0f);
                PlayerController.instance.RestoreFullHP();
            }
        }
    }

    private IEnumerator DelayedGoToNextChapter()
    {
        yield return new WaitForSeconds(0.7f); // 1초 대기 (지연 시간)
        GameManager.instance.GoToChapter(nextChapter);
    }

    private void ChangeBool()
    {
        isChangeTime = false;
    }
}