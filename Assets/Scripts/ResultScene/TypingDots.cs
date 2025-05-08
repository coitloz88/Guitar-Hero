using System.Collections;
using UnityEngine;
using TMPro; // TextMeshProUGUI 사용을 위해 추가

public class TypingDots : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiText;
    private string baseText = "■ Continue";
    private int dotCount = 0;

    void Start()
    {
        StartCoroutine(AnimateDots());
    }

    private IEnumerator AnimateDots()
    {
        while (true)
        {
            uiText.text = baseText + new string('.', dotCount);
            dotCount = (dotCount + 1) % 4;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
