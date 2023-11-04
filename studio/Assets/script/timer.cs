using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private TextMeshProUGUI PhaseText;
    private float elapsedTime;
    private int seconds;
    private int phase;

    // Update is called once per frame
    void Update()
    {
        if (ResizeObject.GetInstance().begin)
        {
            elapsedTime += Time.deltaTime;
            seconds = Mathf.FloorToInt(elapsedTime % 60);
            TimerText.text = string.Format("{0:00}", seconds);
        }
        
        if (seconds>=1 && seconds <= 3)
        {
            PhaseText.text = "inhale";
        }

        if (seconds>3 && seconds <= 7)
        {
            PhaseText.text = "hold";
        }
        if (seconds>7 && seconds <= 14)
        {
            PhaseText.text = "exhale";
        }
        
        
    }
}
