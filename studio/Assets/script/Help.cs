using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Help : MonoBehaviour
{
    public Image help;
    public bool active = false;

    void Start()
    {
        help.gameObject.SetActive(true);
        Info();
    }

    private void Update()
    {
        
    }

    public void Info()
    {
        active = !active;
        if (active)
        {
            help.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            help.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
        
    }

}
