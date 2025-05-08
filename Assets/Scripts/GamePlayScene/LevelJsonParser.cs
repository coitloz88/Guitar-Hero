using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int level;
    public int timeLimit;
    public string[] chords;
    public int problemCnt;
}

[System.Serializable]
public class LevelDataList
{
    public LevelData[] levels; // LevelData 배열
}

public static class LevelJsonParer
{
    static private List<LevelData> parsedLevelData = new List<LevelData>();
    public static List<LevelData> ParseJSON(string jsonText)
    {
        if (parsedLevelData.Count == 0)
        {
            LevelDataList levelDataListWrapper = JsonUtility.FromJson<LevelDataList>(jsonText);
            parsedLevelData = new List<LevelData>(levelDataListWrapper.levels);
        }
        return parsedLevelData;
       
    }
}