using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfScreenTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Note"))
        {
            var note = collision.gameObject.GetComponent<Note>();
            note.OutOfScreen();
        }
    }
}

//If a Collider with the tag "Note" enters this trigger area, it retrieves the Note component attached to that GameObject and triggers the OutOfScreen() method from the Note script.
//This mechanism manages notes moving out of the screen,  missed notes .