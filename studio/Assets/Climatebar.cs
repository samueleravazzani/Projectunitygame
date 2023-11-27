using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script attaccato al rispettivo slider per regolare il livello!


public class Climatebar : MonoBehaviour
{
    public Slider slider;
    
    public void SetMaxClimate(float climate)
    {
        slider.maxValue = climate;
    }

    public void SetClimate(float climate)
    {
        slider.value = climate;
    }
}