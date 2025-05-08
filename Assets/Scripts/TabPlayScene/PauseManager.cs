using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!TabManager.isPause)
            {
                CallPauseMenu();
            } else
            {
                ClosePauseMenu();
            }
        }
    }

    private void CallPauseMenu()
    {
        TabManager.isPause = true;
        pauseUI.SetActive(true);
    }

    private void ClosePauseMenu()
    {
        TabManager.isPause = false;
        pauseUI.SetActive(false);
    }
}
