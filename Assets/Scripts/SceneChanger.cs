using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    public void MoveToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void MoveToTrainingMenu()
    { 
        SceneManager.LoadScene("TrainingMenu");
    }
    public void MoveToPracticeMenu()
    {
        SceneManager.LoadScene("PracticeMenu");
    }
    public void MoveToOptionMenu()
    {
        SceneManager.LoadScene("OptionMenu");
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;    // 에디터일 경우 실행 중단
        #else
            Application.Quit();   // 프로그램 종료
        #endif

    }
}
