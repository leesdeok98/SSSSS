/* using UnityEngine;

public class ChapterTrigger : MonoBehaviour
{
    [Header("이 오브젝트와 충돌 시 챕터 전환 설정")]
    [SerializeField] private int fromChapterIndex = 0;
    [SerializeField] private int toChapterIndex = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.LogWarning("충돌 됨");
            ChapterManager.Instance.StartChapterTransition(fromChapterIndex, toChapterIndex);
        }
    }
} */

using UnityEngine;

public class ChapterTrigger : MonoBehaviour
{

    private bool isChangeTime = false;

    private void Start()
    {
        isChangeTime = false;
        GameManager.instance.ResetCoins();
        GameManager.instance.SetRequiredCoins();
        PlayerController.instance.RestoreFullHP();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ChapterManager manager = FindObjectOfType<ChapterManager>();
            if (manager != null && isChangeTime == false)
            {
                isChangeTime = true;
                Debug.Log("sdkfjls");
                manager.TryChangeChapter();
                Invoke("ChangeBool", 10.0f);
            }
            
        }
    }

    public void ChangeBool()
    {
        isChangeTime = false;

    }
}
