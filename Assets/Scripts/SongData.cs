using UnityEngine.UI;

[System.Serializable]
public class SongData
{
    public int id; // 노래 고유 id
    public string title;   // 제목
    public int difficulty; // 난이도
    public int combo;
    Image songImage;
    
    public SongData(string title, int difficulty)
    {
        // TODO: dummy data 사용 수정
        this.id = 1;
        this.combo = 100;
        this.difficulty = difficulty;
        this.title = title;
    }

    public SongData(int id, string title, int difficulty, int combo) {
        this.id = id;
        this.difficulty = difficulty;
        this.title = title;
        this.combo = combo;
    }

    public void copyData(SongData s)
    {
        this.id = s.id;
        this.title = s.title;
        this.difficulty = s.difficulty;
        this.songImage = s.songImage;
        this.combo = s.combo;
    }
}