using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToInitializeGame1 : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player") && GameManager.instance.questionnairedone)
        {
            Debug.Log("SALVATIIIIIIIII");
            GameManager.instance.InitializeGame();
            GameManager.instance.Save();
        }
    }
}
