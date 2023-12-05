using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNotesTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameController.Instance.lastSpawnedNote != null && collision.gameObject == GameController.Instance.lastSpawnedNote.gameObject)
        {
            GameController.Instance.SpawnNotes();
        }
    }
}

//It checks if there is a collision with a last spawned note so that if new notes are spawned
//Infact the spawining is made at groups of 20 
//So if the 20th marked as last note colllides with the spawn collider 
//then the spawn method is called to generate other 20 notes 