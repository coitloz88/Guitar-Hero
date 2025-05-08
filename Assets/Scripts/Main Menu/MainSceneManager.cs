using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BackgroundMusicManager.Instance.PlayMainMenuBGM();   
    }
}
