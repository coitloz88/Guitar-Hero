using Leap;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckNumber : MonoBehaviour
{
    Controller leapController;
    [SerializeField] private TextMeshProUGUI toPlayText;
    [SerializeField] private TextMeshProUGUI resultText;
    private bool isCorrect = false;     // 일치 여부 체크
    private int targetNumber; // 목표 숫자 (1-5)
    private float timer = 0f;
    float delaytimer = 0f;
    private float timeLimit = 3f; // 제한 시간 3초
    // private int roundNumber = 0; // 횟수

    public void Start() {
        StartNewRound();
    }

    public void Update() {
        timer += Time.deltaTime;

        // 성공 시, 성공 출력 후 새 라운드 시작
        if (isCorrect) {
            resultText.text = "성공!";
            delaytimer += Time.deltaTime;
            if (delaytimer > 1f) {
                StartNewRound();
            }
        }

        // 3초 내에 실패할 경우, 실패 출력 후 새 라운드 시작
        if (timer > timeLimit) {
            if (!isCorrect) {
                resultText.text = "실패...";
                delaytimer += Time.deltaTime;
                if (delaytimer > 1f) {
                    StartNewRound();
                }
            }
        }   
    }

    // 새로운 라운드 시작
    public void StartNewRound() {
        
        isCorrect = false;
        targetNumber = Random.Range(1, 6); // 1~5 사이의 랜덤 숫자 생성
        resultText.text = "????";
        toPlayText.text = targetNumber.ToString();
        timer = 0f;
        delaytimer = 0f;
    }

    // 손 모양 감지 함수 (예시)
    public void DetectHandShape(string inputString) {
        Debug.Log(inputString);
        if (timer < timeLimit) {
            if (inputString == /*"Test0" + */ targetNumber.ToString())
                isCorrect = true;
        }
        else {
            isCorrect = false;
        }
    }
}
