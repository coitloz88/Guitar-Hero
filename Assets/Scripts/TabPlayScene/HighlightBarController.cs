using System.Collections;
using UnityEngine;

public class HighlightBarController : MonoBehaviour
{
    private RectTransform highlightBar; // ���̶���Ʈ ���� RectTransform
    public float moveDuration = 4f; // �̵��ϴ� �� �ɸ��� �ð�
    private float startPositionX;
    private float endPositionX;

    void Awake()
    {
        highlightBar = GetComponent<RectTransform>(); // ������Ʈ �ʱ�ȭ
    }

    public void Initialize(float startX, float endX)
    {
        startPositionX = startX;
        endPositionX = endX;
        highlightBar.anchoredPosition = new Vector2(startX, highlightBar.anchoredPosition.y); // �ʱ� ��ġ ����
    }

    public void StartMoving()
    {
        StopCoroutine("MoveHighlightBar"); // ���� �ڷ�ƾ ����
        StartCoroutine(MoveHighlightBar()); // ���ο� �ڷ�ƾ ����
    }

    private IEnumerator MoveHighlightBar()
    {
        float elapsedTime = 0f;
        float startPos = startPositionX;
        float endPos = endPositionX;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            highlightBar.anchoredPosition = new Vector2(Mathf.Lerp(startPos, endPos, t), highlightBar.anchoredPosition.y); // ��ġ ����
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        highlightBar.anchoredPosition = new Vector2(endPos, highlightBar.anchoredPosition.y); // ������ ��ġ ����
    }
}
