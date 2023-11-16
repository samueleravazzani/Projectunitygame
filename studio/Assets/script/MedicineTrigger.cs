using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MedicineTrigger : MonoBehaviour
{
    public Medicine_Card medicine;
    private void Update()
    {
        if (CardDisplay.instance.showable && Input.GetKeyDown(KeyCode.Space)) // se è in range e preme spazio -> show
        {
            CardDisplay.instance.ShowCard(medicine);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll) // se è in range può mostrare
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            CardDisplay.instance.showable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D coll) // se esce dal range non può mostrare
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            CardDisplay.instance.showable = false;
        }
    }
}
