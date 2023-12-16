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
    [SerializeField] private GameObject phasepanel;
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

    private void Start()
    {
        phasepanel.gameObject.SetActive(false);
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
                phasepanel.gameObject.SetActive(false);
                PhaseText.text = "";
            }

            if (seconds>=1 && seconds <= 4)
            {
                        PhaseText.text = "inhale";
                        phasepanel.gameObject.SetActive(true);
                        TimerText.text = string.Format("{0:00}", seconds);
            }
            
            if (seconds>4 && seconds <= 11)
            {
                        PhaseText.text = "hold";
                        TimerText.text = string.Format("{0:00}", seconds-4);
            }
            if (seconds>11 && seconds <= 19)
            {
                        PhaseText.text = "exhale";
                        TimerText.text = string.Format("{0:00}", seconds-11);
            }
            
            if (seconds == 20)
            {
                        phasepanel.gameObject.SetActive(false);
                        PhaseText.text = "";
                        TimerText.text = "00";
                        elapsedTime = 0;
            }
            

        }

    }
}
