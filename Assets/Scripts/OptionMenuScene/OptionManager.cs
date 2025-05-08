using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private Button minusButton;
    [SerializeField] private Button plusButton;
    [SerializeField] private TextMeshProUGUI timeLimitOptionText;

    private int timeLimitOption = 0;

    private void Start()
    {
        // 현재 볼륨을 슬라이더에 설정
        volumeSlider.value = BackgroundMusicManager.Instance.GetVolume();
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // 현재 timeLimit 옵션을 TMPro에 설정
        timeLimitOption = PlayerPrefs.GetInt("TimeLimitOption", 0);
        timeLimitOptionText.text = timeLimitOption.ToString();
    }

    private void SetVolume(float volume)
    {
        BackgroundMusicManager.Instance.SetVolume(volume);
    }

    public void DecreaseTimeLimitOption()
    {
        if (timeLimitOption > -2) 
        {
            PlayerPrefs.SetInt("TimeLimitOption", --timeLimitOption);
            timeLimitOptionText.text = timeLimitOption.ToString();
        }
    }

    public void IncreaseTimeLimitOption()
    {
        if (timeLimitOption < 9)
        {
            PlayerPrefs.SetInt("TimeLimitOption", ++timeLimitOption);
            timeLimitOptionText.text = timeLimitOption.ToString();
        }
    }
}
