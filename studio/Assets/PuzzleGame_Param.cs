using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleGame_Param : MonoBehaviour
{
    public void Parametrization()
    {
        if (GameManager.instance.anxiety >= 1 && GameManager.instance.anxiety <= 3)
        {
            PlayerPrefs.SetInt("AnxietyLevel", 1);
        }
        else if (GameManager.instance.anxiety >= 4 && GameManager.instance.anxiety <= 6)
        {
            PlayerPrefs.SetInt("AnxietyLevel", 2);
        }
        else if (GameManager.instance.anxiety >= 7 && GameManager.instance.anxiety <= 9)
        {
            PlayerPrefs.SetInt("AnxietyLevel", 3);
        }

        SceneManager.LoadScene("PuzzleGameSelectLevel"); 
    }
}

