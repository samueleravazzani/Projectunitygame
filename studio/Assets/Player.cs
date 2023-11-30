using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//QUESTO IN REALTA' E' LO SCRIPT CHE DEVE ANDARE NEL GAMEMANAGER PER GESTIRE GLI SLIDER
//AL MOMENTO MESSO QUI PER CAPIRE COME DOVESSE FUNZIONARE!

public class Player : MonoBehaviour
{
    public BarManager anxietyBar;
    public BarManager literacyBar;
    public BarManager climateBar;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anxietyBar.TakeDamage(1.0f);
            literacyBar.TakeDamage(2.0f);
            climateBar.TakeDamage(3.0f);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            anxietyBar.TakeIncrement(1.0f);
            literacyBar.TakeIncrement(2.0f);
            climateBar.TakeIncrement(3.0f);
        }
    }
}