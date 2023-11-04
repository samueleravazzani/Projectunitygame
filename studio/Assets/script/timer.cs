using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimerText;
    private float elapsedTime;

    // Update is called once per frame
    void Update()
    {
        if (ResizeObject.GetInstance().begin)
        {
                elapsedTime += Time.deltaTime;
                int seconds = Mathf.FloorToInt(elapsedTime % 60);
                TimerText.text = string.Format("{0:00}", seconds);
            }
            
        }
    }
}
