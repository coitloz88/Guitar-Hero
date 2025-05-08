using Leap;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject EndUI;
    [SerializeField] private Image chordImage; // 코드 이미지를 표시할 UI 이미지
    [SerializeField] private TextMeshProUGUI scoreText; // 점수를 표시할 UI 텍스트
    [SerializeField] private TextMeshProUGUI comboText; // 연속 정답 수를 표시할 UI 텍스트
    [SerializeField] private TextMeshProUGUI chordName; // 코드 이름을 표기할 UI 텍스트
    [SerializeField] private TextMeshProUGUI second; // 시간 제한을 표기할 UI 텍스트
    [SerializeField] private GameObject OXPopup;
    [SerializeField] private TextMeshProUGUI resultText;

    [SerializeField] private TextAsset jsonFile;

    [SerializeField] private GameEffectManager gameEffectManager;

    [SerializeField] private GameObject poseViewer;   // 목표 운지 손 자세 목록의 부모 오브젝트
    [SerializeField] private GameObject poseDetector; // 손 동작 탐지 목록의 부모 오브젝트

    [SerializeField] private GameObject prefabPoseViewer;   // 목표 운지 손 자세 프리팹
    [SerializeField] private GameObject prefabPoseDetector; // 손 동작 탐지 프리팹

    private List<GameObject> poseDetectorList = new List<GameObject>();     // 목표 운지 손 자세 리스트
    private List<GameObject> poseViewerList = new List<GameObject>();       // 손 동작 탐지 리스트

    private int timeLimit = 5; // 난이도 별 시간 달리함
    private int problemCnt = 0;
    private String[] chordDatas;
    private Sprite[] chordSprites;
    private int score = 0;
    private int combo = 0;
    private int maxCombo = 0;
    private String currentChord = "";
    private GameObject goalHandPose;        // 현재 목표 손 동작

    private const int DEFAULT_SCORE = 1000;

    private bool isPause = false;
    private bool isCorrect = false;

    private Coroutine gameLoopCoroutine;

    void Start()
    {
        BackgroundMusicManager.Instance.PlayGameplayBGM();

        if (jsonFile != null)
        {
            // json 파일의 내용을 읽어서 파싱
            List<LevelData> levelData = LevelJsonParer.ParseJSON(jsonFile.text);
            
            int selectedLevel = PlayerPrefs.GetInt("SelectedLevel", 1);
            // 레벨 데이터 출력
            foreach (var level in levelData)
            {
                if (level.level == selectedLevel)
                {
                    timeLimit = level.timeLimit + PlayerPrefs.GetInt("TimeLimitOption", 0);
                    problemCnt = level.problemCnt;
                    chordDatas = level.chords;
                    ShuffleArray(chordDatas);
                    chordSprites = LoadChordsSprites(chordDatas);
                    SelectChordList(chordDatas);

                    break;
                }
            }

            if (chordDatas == null || chordSprites == null)
            {
                // parsing Error
                Debug.LogError("JSON 파일이 파싱되지 않았습니다.");
                // TODO: Level Select Scene으로 다시 넘어감
            }



            gameLoopCoroutine = StartCoroutine(GameLoop());
        }
        else
        {
            Debug.LogError("JSON 파일이 연결되지 않았습니다.");
            // TODO: Level Select Scene으로 다시 넘어감
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                CallPauseMenu();
            }
            else
            {
                ClosePauseMenu();
            }
        }
    }
    Sprite[] LoadChordsSprites(string[] chords)
    {
        List<Sprite> sprites = new List<Sprite>();

        foreach (var chord in chords)
        {
            string spriteName = $"Chord-{chord}";
            Sprite sprite = Resources.Load<Sprite>(spriteName);

            if (sprite != null)
            {
                sprites.Add(sprite);
            }
            else
            {
                Debug.LogWarning($"Sprite '{spriteName}' not found.");
            }
        }

        return sprites.ToArray<Sprite>();
    }

    private void CallPauseMenu()
    {
        isPause = true;
        StopCoroutine(gameLoopCoroutine);
        pauseUI.SetActive(true);
    }

    private void ClosePauseMenu()
    {
        isPause = false;
        pauseUI.SetActive(false);
        gameLoopCoroutine = StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        int length = chordDatas.Length;

        for (int i  = 0; i < problemCnt; i++) {
            UpdateChordImage(i % length); // 질문에 대한 이미지 업데이트
            yield return StartCoroutine(Countdown()); // 카운트다운 시작
            OXPopup.SetActive(true);
            gameEffectManager.PlayProblemVFX(isCorrect);
            isCorrect = false;
            yield return new WaitForSeconds(0.5f); // 연속 입력 방지를 위해 대기
            OXPopup.SetActive(false);
            goalHandPose.SetActive(false);
        }
        // TODO: 종료 처리
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(HandleResult());
    }

    private IEnumerator Countdown()
    {
        float timeRemaining = timeLimit;

        while (timeRemaining > 0)
        {
            second.text = timeRemaining.ToString("F0"); // 남은 시간 표시
            timeRemaining -= Time.deltaTime;

            // 입력 체크
            if (Input.GetKeyDown(KeyCode.O) || isCorrect)
            {
                CheckAnswer(isCorrect);
                yield break;
            }
            yield return null; // 다음 프레임까지 대기
        }
        
        HandleWrongAnswer(); // 시간이 다 되면 틀린 것으로 간주
    }

    private IEnumerator HandleResult()
    {
        PlayerPrefs.SetInt("GameScore", score);
        PlayerPrefs.SetInt("GameCombo", maxCombo);

        EndUI.SetActive(true);

        bool result = score > problemCnt * DEFAULT_SCORE / 2;

        if (result)
        {
            resultText.text = "Success!";
        }
        else
        {
            resultText.text = "Failed...";
        }

        gameEffectManager.gameObject.SetActive(true);
        yield return null; // 한 프레임 대기

        gameEffectManager.PlayResultEffect(result);

        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("ResultScene");
    }

    private void HandleWrongAnswer()
    {
        combo = 0;
        isCorrect = false;
        OXPopup.GetComponent<TextMeshProUGUI>().text = "X";
        Debug.Log("틀렸습니다...");
    }

    private void UpdateChordImage(int chordIdx)
    {
        chordImage.sprite = chordSprites[chordIdx];
        currentChord = chordSprites[chordIdx].name.Split('-')[1];
        chordName.text = currentChord + " 코드";

        //질문 코드에 대한 참고 손 동작 오브젝트 활성화
        goalHandPose = poseViewer.transform.Find(currentChord + " Pose").gameObject;
        goalHandPose.SetActive(true);
    }

    private void CheckAnswer(bool playerAnswer)
    {
        if (isCorrect)
        {
            score += (combo + 1) * DEFAULT_SCORE;
            combo++;
            maxCombo = combo > maxCombo ? combo : maxCombo;
            OXPopup.GetComponent<TextMeshProUGUI>().text = "O";
            Debug.Log("맞았습니다!");
        }
        else
        {
            HandleWrongAnswer();
        }

        UpdateNoteInfoUI();
    }

    private void UpdateNoteInfoUI()
    {
        scoreText.text = score.ToString("D6");
        comboText.text = combo.ToString("D3");
    }

    public void DetectHandShape(string inputString) {
        Debug.Log(inputString + "/" + currentChord);
        if (inputString == currentChord)
            isCorrect = true;
        else
            isCorrect = false;
    }

    // 선택 레벨에 따른 일부 코드의 Detector와 Viewer 프리팹 설정
    public void SelectChordList(string[] chordData)
    {
        foreach (string chord in chordData)
        {
            GameObject viewer = Instantiate(prefabPoseViewer, poseViewer.transform);
            viewer.name = chord + " Pose";

            // Resources 폴더에서 손 동작 에셋을 로드
            HandPoseScriptableObject handPose = Resources.Load<HandPoseScriptableObject>("HandPoses/" + chord);
            if (handPose == null)
            {
                Debug.LogError($"HandPose asset for {chord} not found in Resources/HandPoses/");
                continue;
            }

            // handPose 변수에 할당
            viewer.GetComponent<HandPoseViewer>().handPose = handPose;

            viewer.SetActive(false);
            poseDetectorList.Add(viewer);
        }
    }

    void ShuffleArray(string[] array)
    {
        System.Random rng = new System.Random();
        int n = array.Length;

        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(0, i + 1);
            // 원소 스왑
            string temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}