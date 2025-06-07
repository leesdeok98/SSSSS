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
