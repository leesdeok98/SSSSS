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
                    GameManager.instance.GoToChapter(nextChapter);
                    StartCoroutine(AudioManager.instance.MusicFadeout());
                    AudioManager.instance.StopBGM();
                    return;
                }

                isChangeTime = true;
                manager.TryChangeChapter();
                Invoke("ChangeBool", 10.0f);
                GameManager.instance.GoToChapter(nextChapter);
                PlayerController.instance.RestoreFullHP();
            }
        }
    }

    public void ChangeBool()
    {
        isChangeTime = false;
    }
}
