using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMinigames : MonoBehaviour
{
    public Button PauseMinigame;
    public Image pausepanel;
    public Button backtoplay;
    public Button toforest;

    void Awake()
    {
        pausepanel.gameObject.SetActive(false);
    }
    
    public void PauseMenu()
    {
        pausepanel.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }
    
    public void GoBackToPlay()
    {
        pausepanel.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
    
    public void ToForest()
    {
        Time.timeScale = 1.0f;
        // e chiamo TaskFailedAndActivateChangeScene dello SceneSlave
        pausepanel.gameObject.SetActive(false);
        PauseMinigame.gameObject.SetActive(false);
    }
    
    
}
