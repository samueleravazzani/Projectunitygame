using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinPopUp : MonoBehaviour
{
    public GameObject winPopup;
    void Start()
    {
        winPopup.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.OnBoardCompleted += ShowWinPopup;
        Debug.Log("WinPopUp subscribed to OnBoardCompleted event.");
    }


    private void OnDisable()
    {
        GameEvents.OnBoardCompleted -= ShowWinPopup;
    }

    private void ShowWinPopup()
    {
        Debug.Log("Tutte le parole trovate. Attivazione del popup.");
        winPopup.SetActive(true);
    }


    public void LoadNextLevel()
    {
        GameEvents.LoadNextLevelMethod();
    }
}
