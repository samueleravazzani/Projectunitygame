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
        slider.value = currentValue;
    }

    public void TakeDamage(float damage)
    {
        currentValue -= damage;
        SetValue(currentValue);
    }

    public void TakeIncrement(float increment)
    {
        currentValue += increment;
        SetValue(currentValue);
    }
}