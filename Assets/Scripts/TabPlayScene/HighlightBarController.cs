using System.Collections;
using UnityEngine;

public class HighlightBarController : MonoBehaviour
{
    private RectTransform highlightBar; // 하이라이트 바의 RectTransform
    public float moveDuration = 4f; // 이동하는 데 걸리는 시간
    private float startPositionX;
    private float endPositionX;

    void Awake()
    {
        highlightBar = GetComponent<RectTransform>(); // 컴포넌트 초기화
    }

    public void Initialize(float startX, float endX)
    {
        startPositionX = startX;
        endPositionX = endX;
        highlightBar.anchoredPosition = new Vector2(startX, highlightBar.anchoredPosition.y); // 초기 위치 설정
    }

    public void StartMoving()
    {
        StopCoroutine("MoveHighlightBar"); // 이전 코루틴 정지
        StartCoroutine(MoveHighlightBar()); // 새로운 코루틴 시작
    }

    private IEnumerator MoveHighlightBar()
    {
        float elapsedTime = 0f;
        float startPos = startPositionX;
        float endPos = endPositionX;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            highlightBar.anchoredPosition = new Vector2(Mathf.Lerp(startPos, endPos, t), highlightBar.anchoredPosition.y); // 위치 보간
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        highlightBar.anchoredPosition = new Vector2(endPos, highlightBar.anchoredPosition.y); // 마지막 위치 설정
    }
}
