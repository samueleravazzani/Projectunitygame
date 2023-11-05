using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class timer : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI TimerText;
    [SerializeField] private TextMeshProUGUI PhaseText;
    [SerializeField] private TextMeshProUGUI RepeatText;
    private float elapsedTime;
    public int seconds { get; private set; }
    private int phase;
    private int repetition;
    
    private static timer instance;

    private void Awake() //creation singleton
    {
        instance = this;
    }

    public static timer GetInstance()
    {
        return instance;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (ResizeObject.GetInstance().begin)
        {
            elapsedTime += Time.deltaTime;
            seconds = Mathf.FloorToInt(elapsedTime % 60);
            if (seconds == 0)
            {
                TimerText.text = "GO!";
                PhaseText.text = "";
            }

            if (seconds>=1 && seconds <= 3)
            {
                        PhaseText.text = "inhale";
                        TimerText.text = string.Format("{0:00}", seconds);
            }
            
            if (seconds>3 && seconds <= 7)
            {
                        PhaseText.text = "hold";
                        TimerText.text = string.Format("{0:00}", seconds-3);
            }
            if (seconds>7 && seconds <= 14)
            {
                        PhaseText.text = "exhale";
                        TimerText.text = string.Format("{0:00}", seconds-7);
            }
            
            if (seconds == 15)
            {
                        PhaseText.text = "";
                        TimerText.text = "00";
                        elapsedTime = 0;
            }
            

        }

    }
}
