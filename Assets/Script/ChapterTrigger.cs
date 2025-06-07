/* using UnityEngine;

public class ChapterTrigger : MonoBehaviour
{
    [Header("�� ������Ʈ�� �浹 �� é�� ��ȯ ����")]
    [SerializeField] private int fromChapterIndex = 0;
    [SerializeField] private int toChapterIndex = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogWarning("�浹 ��");
            ChapterManager.Instance.StartChapterTransition(fromChapterIndex, toChapterIndex);
        }
    }
} */

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
            Debug.Log($"é�� �浹: {other.gameObject.name}");

            ChapterManager manager = FindObjectOfType<ChapterManager>();
            if (manager != null && isChangeTime == false)
            {
                isChangeTime = true;
                Debug.Log("sdkfjls");
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
