using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TabManager : MonoBehaviour
{
    [SerializeField] private GameObject SongTitleText;
    [SerializeField] private GameObject ScoreText;
    [SerializeField] private GameObject ComboText;
    [SerializeField] private GameObject highlightBarObject;
    [SerializeField] private TextMeshProUGUI[] tabLines;

    public static bool isPause = false;

    private float displayDuration = 4f; // �� �Ǻ� ǥ�� �ð�
    private string[][] tabs;
    private int currentMeasure = 0;
    private float tabWidth = 1430f; // Ÿ�� �Ǻ��� ���� ����

    private HighlightBarController highlightBarController;

    // Start is called before the first frame update
    void Start()
    {
        SongTitleText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("SelectedSongTitle", "NULL");
        highlightBarController = highlightBarObject.GetComponent<HighlightBarController>();

        tabs = new string[][]
       {
            new string[]
            {
                "e|---0---|-------|---1---|---0---|---0---|-------|---1---|---0---|", // 1st string
                "B|---1---|-------|-------|---1---|---1---|-------|-------|---1---|", // 2nd string
                "G|---0---|-------|-------|---0---|---0---|-------|-------|---0---|", // 3rd string
                "D|---2---|---0---|---2---|---2---|---2---|---0---|---2---|---2---|", // 4th string
                "A|---3---|-------|---2---|---0---|---3---|-------|---2---|---0---|", // 5th string
                "E|-------|-------|-------|-------|-------|-------|-------|-------|", // 6th string
            },
            new string[]
            {
                "e|---3---|---0---|-------|---3---|---0---|---0---|---1---|---3---|", // 1st string
                "B|---1---|---1---|-------|---0---|---1---|---1---|---3---|---0---|", // 2nd string
                "G|---3---|---2---|-------|---0---|---2---|---2---|---2---|---0---|", // 3rd string
                "D|-------|---2---|-------|---2---|---2---|---2---|---0---|---2---|", // 4th string
                "A|-------|---0---|-------|---2---|---0---|---0---|-------|---2---|", // 5th string
                "E|-------|-------|-------|---0---|-------|-------|---3---|---0---|", // 6th string
            },
            new string[]
            {
                "e|---0---|---2---|---3---|-------|---0---|---2---|---3---|-------|", // 1st string
                "B|---2---|---3---|---0---|-------|---2---|---3---|---0---|-------|", // 2nd string
                "G|---2---|---2---|---0---|---0---|-------|-------|-------|-------|", // 3rd string
                "D|---2---|---0---|---2---|---2---|-------|-------|-------|-------|", // 4th string
                "A|---0---|-------|---2---|---0---|-------|-------|-------|-------|", // 5th string
                "E|-------|-------|-------|-------|---0---|---2---|---3---|-------|", // 6th string
            },
            new string[]
            {
                "e|-------|---2---|---1---|---0---|---0---|-------|---1---|---0---|", // 1st string
                "B|---2---|---3---|-------|---1---|---1---|-------|-------|---1---|", // 2nd string
                "G|---2---|---2---|-------|---0---|---0---|-------|-------|---0---|", // 3rd string
                "D|---2---|-------|---2---|---2---|---2---|---0---|---2---|---2---|", // 4th string
                "A|-------|-------|---2---|---0---|---3---|-------|---2---|---0---|", // 5th string
                "E|-------|---2---|-------|-------|-------|-------|-------|-------|", // 6th string
            },
       };

        // ���̶���Ʈ ���� �̵� ���� �ʱ�ȭ.
        float startPosX = 135; // Ÿ�� �Ǻ��� ���� ��
        float endPosX = tabWidth + 115; // Ÿ�� �Ǻ��� ������ ��

        highlightBarController.moveDuration = displayDuration;
        highlightBarController.Initialize(startPosX, endPosX);

        // �ʱ� Ÿ�� ���� ����
        SetTabLines(currentMeasure);

        StartPlaying();
    }

    void SetTabLines(int measureIndex)
    {
        for (int i = 0; i < tabLines.Length; i++)
        {
            tabLines[i].text = tabs[measureIndex][i];
        }
    }

    void StartPlaying()
    {
        StartCoroutine(PlayTabs());
    }

    IEnumerator PlayTabs()
    {
        int totalMeasures = tabs.Length;
        for (int i = 0; i < totalMeasures; i++)
        {
            highlightBarController.StartMoving();
            yield return new WaitForSeconds(displayDuration); // �� ���� ǥ�� �ð�
            SetTabLines(i);
        }   

        EndScene();
    }

    void EndScene()
    {
        StartCoroutine(ExitAfterDelay(1f));
    }

    IEnumerator ExitAfterDelay(float delay)
    {
        highlightBarController.StartMoving();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("ResultScene");
    }

    // Update is called once per frame
    void Update()
    {
        //if (isPause)
        //{
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //}
    }
}
