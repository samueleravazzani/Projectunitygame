using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//script attaccato al rispettivo slider per regolare il livello!

public class Literacybar : MonoBehaviour
{
    public Slider slider;

    
    public void SetMaxLiteracy(float literacy)
    {
        slider.maxValue = literacy;
    }

    public void SetLiteracy(float literacy)
    {
        slider.value = literacy;
    }
}