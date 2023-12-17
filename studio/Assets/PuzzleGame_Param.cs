using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleGame_Param : MonoBehaviour
{
    public void Parametrization()
    {
        if (Mathf.RoundToInt(GameManager.instance.anxiety) >= 1 && Mathf.RoundToInt(GameManager.instance.anxiety) <= 3)
        {
            PlayerPrefs.SetInt("AnxietyLevel", 1);
        }
        else if (Mathf.RoundToInt(GameManager.instance.anxiety) >= 4 && Mathf.RoundToInt(GameManager.instance.anxiety) <= 6)
        {
            PlayerPrefs.SetInt("AnxietyLevel", 2);
        }
        else if (Mathf.RoundToInt(GameManager.instance.anxiety) >= 7 && Mathf.RoundToInt(GameManager.instance.anxiety) <= 10)
        {
            PlayerPrefs.SetInt("AnxietyLevel", 3);
        }

        SceneManager.LoadScene("PuzzleGameSelectLevel"); 
    }
}

