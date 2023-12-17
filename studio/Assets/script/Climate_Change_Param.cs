using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Climate_Change_Param : MonoBehaviour
{
    public void Start()
    {
        Debug.Log(GameManager.instance.climate_change_skept);
    }

    public void Parametrization()
    {
        if (Mathf.RoundToInt(GameManager.instance.climate_change_skept) >= 1 && Mathf.RoundToInt(GameManager.instance.climate_change_skept) <= 3)
        {
            Debug.Log("Arrivato in Climate_Change_Fuoco");
            PlayerPrefs.SetInt("CCSLevel", 1);
            SceneManager.LoadScene("Climate_Change_Fuoco");
        }
        if (Mathf.RoundToInt(GameManager.instance.climate_change_skept) >= 4 && Mathf.RoundToInt(GameManager.instance.climate_change_skept) <= 6)
        {
            SceneManager.LoadScene("Climate_Change_Fuoco1");
            PlayerPrefs.SetInt("CCSLevel", 2);
        }
        if (Mathf.RoundToInt(GameManager.instance.climate_change_skept) >= 7 && Mathf.RoundToInt(GameManager.instance.climate_change_skept) <= 10)
        {
            SceneManager.LoadScene("Climate_Change_Fuoco2");
            PlayerPrefs.SetInt("CCSLevel", 3);
        }
 
    }
}
