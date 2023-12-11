using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToInitializeGame1 : MonoBehaviour
{
    public void OnTriggerExit2D(Collider2D player)
    {
        if (player.CompareTag("Player") && GameManager.instance.questionnairedone)
        {
            GameManager.instance.InitializeGame();
        }
    }
}
