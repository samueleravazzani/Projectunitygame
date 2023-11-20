using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPopUp : MonoBehaviour
{
    public GameObject gameOverPopup;
    public GameObject wordgrid;
    
    void Start()
    {
        gameOverPopup.SetActive(false);
        GameEvents.OnGameOver += ShowGameOverPopup;
    }
    
    private void OnDisable()
    {
        GameEvents.OnGameOver -= ShowGameOverPopup;
    }

    private void ShowGameOverPopup()
    {
        wordgrid.SetActive(false);
        gameOverPopup.SetActive(true);
    }
    
}
