using UnityEngine;

public class ChapterTrigger : MonoBehaviour
{
    [Header("이 오브젝트와 충돌 시 챕터 전환 설정")]
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
