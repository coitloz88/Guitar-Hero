using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public GameObject PopUpPanel;
    public GameObject SongInformation;
    SongData selectedSongData;

    private void Start()
    {
        selectedSongData = new SongData("Unknown song", 10);
    }

    public void ShowPopUp(SongData songData)
    {
        selectedSongData.copyData(songData);
        SongInformation.GetComponent<TextMeshProUGUI>().text = selectedSongData.title;
        PopUpPanel.SetActive(true);
    }

    public void OnClickConfirmButton() 
    {
        PlayerPrefs.SetInt("SelectedSongId", selectedSongData.id);
        PlayerPrefs.SetString("SelectedSongTitle", selectedSongData.title);
        PlayerPrefs.Save();

        SceneManager.LoadScene("GamePlayScene");
    }

    public void OnClickCancelButton()
    {
        PopUpPanel.SetActive(false);
    }
}
