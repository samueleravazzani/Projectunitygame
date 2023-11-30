using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    Anxiety,
    Literacy,
    Climate
}

public class BarManager : MonoBehaviour
{
    public Slider slider;
    public BarType barType;
    
    private float maxValue = 10.0f;
    private float currentValue;

    void Start()
    {
        switch (barType)
        {
            case BarType.Anxiety:
                currentValue = slider.value; /* Value from slider */;
                break;
            case BarType.Literacy:
                currentValue = slider.value;  /* Value from slider */;
                break;
            case BarType.Climate:
                currentValue = slider.value; /* Value from slider */;
                break;
        }

        SetMaxValue(maxValue);
        SetValue(currentValue);
    }

    public void SetMaxValue(float value)
    {
        maxValue = value;
        slider.maxValue = value;
    }

    public void SetValue(float value)
    {
        currentValue = Mathf.Clamp(value, 1f, maxValue);
        float invertedValue = maxValue - (currentValue - 1f); // in questo moodo se il currengt value dell'ansia è 1 allora il valore che lo slider vedrà è 10 
        //l'idea infatti è che se hai poca ansia stai bene per cui lo slider è pieno 
        //quando invece il valore della tua ansia è a 10  allora il valore dello slider è a 1 quindi è basso
        // per cui l'obiettivo è riempire lo slider 
        slider.value = invertedValue;
    }

    public void TakeDamage(float damage) // il damage diminuisce per cui lo slider aumenta quindi questa va chiamata quando la task viene svolta correttamente
    {
        currentValue -= damage;
        SetValue(currentValue);
    }

    public void TakeIncrement(float increment) //esatto contrario 
    {
        currentValue += increment;
        SetValue(currentValue);
    }
}