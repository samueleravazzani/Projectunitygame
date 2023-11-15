using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MedicineTrigger : MonoBehaviour
{
    public Medicine_Card medicine;
    
    private void OnTriggerStay2D(Collider2D coll)
    {
        medicine = GetComponent<Medicine_Card>();
        if (coll.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
        {
            CardDisplay.ShowCard(medicine);
        }
    }
}
