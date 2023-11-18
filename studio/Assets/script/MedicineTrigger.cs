using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MedicineTrigger : MonoBehaviour
{
    public Medicine_Card medicine;
    public bool showable=false;
    private bool itischosen = false;
    
    private void Update()
    {
        if (showable && Input.GetKeyDown(KeyCode.Space)) // se è in range e preme spazio -> show
        {
            CardDisplay.instance.ShowCard(medicine);
            // Debug.Log(medicine.name);
        }

        if (DisplayEnigma.instance.chosen_medicine == medicine)
        {
            itischosen = true;
        }
    }

    
    
    private void OnTriggerEnter2D(Collider2D coll) // se è in range può mostrare
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            showable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D coll) // se esce dal range non può mostrare
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            showable = false;
        }
    }
}