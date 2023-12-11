using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Climate_Change_Param : MonoBehaviour
{
    public void Parametrization()
    {
        if (GameManager.instance.climate_change_skept >= 1 && GameManager.instance.climate_change_skept <= 3)
        {
            Debug.Log("Arrivato in Climate_Change_Fuoco");
            SceneManager.LoadScene("Climate_Change_Fuoco");
        }
        else if (GameManager.instance.climate_change_skept >= 4 && GameManager.instance.climate_change_skept <= 6)
        {
            SceneManager.LoadScene("Climate_Change_Fuoco1");
        }
        else if (GameManager.instance.climate_change_skept >= 7 && GameManager.instance.climate_change_skept <= 9)
        {
            SceneManager.LoadScene("Climate_Change_Fuoco2");
        }
 
    }
}
