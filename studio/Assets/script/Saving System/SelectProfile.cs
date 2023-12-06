using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectProfile : MonoBehaviour
{
    public string profileName;
    public TextMeshProUGUI profileSelected;
    
    public void LoadProfile()
    {
        // prendo il nome della casella di testo
        profileName = profileSelected.text;
        // chiamo la funzione Save del GameManager che prende il nome del profilo, crea i dati e li passa al SaveSystem che li salva
        GameManager.instance.Load(profileName);
    }

    /*public void CreateProfile()
    {
        profileName = profileSelected.text;
        GameManager.instance.InitializeGameFirstTime();
    } */
}
