using UnityEngine;

public class ChapterTrigger : MonoBehaviour
{
    [Header("�� ������Ʈ�� �浹 �� é�� ��ȯ ����")]
    [SerializeField] private int fromChapterIndex = 0;
    [SerializeField] private int toChapterIndex = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChapterManager.Instance.StartChapterTransition(fromChapterIndex, toChapterIndex);
        }
    }
}
