using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MedicineTrigger : MonoBehaviour
{
    
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Return))
        {
            CardDisplay.medicine_name = "Paracetamol";
            CardDisplay.showcard = true;
        }
    }
}
