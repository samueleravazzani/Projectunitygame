using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script attaccato al rispettivo slider per regolare il livello!

public class Anxietybar : MonoBehaviour
{
    public Slider slider;

    
    public void SetMaxAnxiety(float anxiety)
    {
        slider.maxValue = anxiety;
    }

    public void SetAnxiety(float anxiety)
    {
        slider.value = anxiety;
    }
}
