using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MedicineTrigger : MonoBehaviour
{
    public Medicine_Card medicine;
    public static bool show=false;
    private void Update()
    {
        if (show)
        {
            CardDisplay.instance.ShowCard(medicine);
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
        {
            show = true;
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
        {
            show = false;
        }
    }
}
