using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameResultAdapter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ComboText;
    [SerializeField] TextMeshProUGUI ScoreText;
    void Awake()
    {
        ComboText.text = PlayerPrefs.GetInt("GameCombo").ToString("D3");
        ScoreText.text = PlayerPrefs.GetInt("GameScore").ToString("D6");
    }
}
