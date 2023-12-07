using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGame_Param1 : MonoBehaviour
{
    public GameObject[] buttonsToDisableLevel1; 
    public GameObject[] buttonsToDisableLevel2; 
    public GameObject[] buttonsToDisableLevel3; 

    void Start()
    {
        int anxietyLevel = PlayerPrefs.GetInt("AnxietyLevel", 1);
        
        switch (anxietyLevel)
        {
            case 1:
                SetButtonsActive(buttonsToDisableLevel1, true);
                SetButtonsActive(buttonsToDisableLevel2, false);
                SetButtonsActive(buttonsToDisableLevel3, false);
                break;
            case 2:
                SetButtonsActive(buttonsToDisableLevel1, true);
                SetButtonsActive(buttonsToDisableLevel2, true);
                SetButtonsActive(buttonsToDisableLevel3, false);
                break;
            case 3:
                SetButtonsActive(buttonsToDisableLevel1, true);
                SetButtonsActive(buttonsToDisableLevel2, true);
                SetButtonsActive(buttonsToDisableLevel3, true);
                break;
            default:
                break;
        }
    }
    
    void SetButtonsActive(GameObject[] buttons, bool active)
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(active);
        }
    }
}