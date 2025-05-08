using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelListViewManager : MonoBehaviour
{
    [SerializeField] private GameObject selectListItemPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private TextAsset jsonFile;
    private List<LevelData> levelData;

    // Start is called before the first frame update
    void Start()
    {
        if(jsonFile != null)
        {
            levelData = LevelJsonParer.ParseJSON(jsonFile.text);
            CreateSongList();
        }
        else
        {
            Debug.LogError("JSON 파일이 연결되지 않았습니다.");
        }
    }

    private void CreateSongList()
    {
        Debug.Log(levelData.Count);
        // 기존 아이템 삭제
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        for (int i = 1; i <= levelData.Count; i++)
        {
            GameObject newItem = Instantiate(selectListItemPrefab, content);
            Button itemButton = newItem.GetComponent<Button>();
            TextMeshProUGUI itemLevelText = newItem.GetComponentsInChildren<TextMeshProUGUI>()[0];
            TextMeshProUGUI itemLimitText = newItem.GetComponentsInChildren<TextMeshProUGUI>()[1];
            TextMeshProUGUI itemChordText = newItem.GetComponentsInChildren<TextMeshProUGUI>()[2];

            if (itemButton != null && itemLevelText != null && itemLimitText != null && itemChordText != null)
            {
                // 아이템 텍스트 설정
                itemLevelText.text = $"LEVEL {i}";
                itemLimitText.text = "제한시간: " + levelData[i-1].timeLimit.ToString() + "초";
                string chordListText = "";
                foreach (string chord in levelData[i-1].chords)
                {
                    chordListText += chord + " ";
                }

                chordListText.Trim();           // 마지막 공백 제거
                itemChordText.text = chordListText;

                int level = i;
                itemButton.onClick.AddListener(() => OnSongButtonClick(level));
            }
        }
    }

    public void OnSongButtonClick(int level)
    {
        PlayerPrefs.SetInt("SelectedLevel", level);
        SceneManager.LoadScene("GamePlayScene");
    }
}
