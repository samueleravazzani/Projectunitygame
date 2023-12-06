using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSave : MonoBehaviour
{
    private bool save=false;

    private void Update()
    {
        if (save)
        {
            if (SceneManager.GetActiveScene().name == "MainMap")
            {
                GameManager.instance.Save();
                save = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        save = true;
    }
}
