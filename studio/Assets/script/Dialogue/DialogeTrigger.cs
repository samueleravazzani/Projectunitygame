using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogeTrigger : MonoBehaviour
{ //script attaccato al trigger dell'NPC
    [Header("Visual Cue")] [SerializeField]
    private GameObject visualCue;

    [Header("Ink JSON")] [SerializeField] private TextAsset[] inkJSON;

    private bool playerInRange;
    private string nameNPC;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        
    }
    private void Update()
    {
        if (playerInRange && !DialogeManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                name = gameObject.tag;
                Debug.Log("sono "+name.ToString());
                DialogeManager.GetInstance().EnterDialogueMode(inkJSON[0]);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }

       /* if (DialogeManager.GetInstance().choiceismade)
        {
            if (DialogeManager.GetInstance().choicemade == 0)
            {
                Debug.Log(" sono albero e ho visto che hai scelto si ");
            }
            else if (DialogeManager.GetInstance().choicemade == 1)
            {
                Debug.Log(" sono albero e ho visto che hai scelto no ");
            }
            
        }
        */
           
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
        
    }
}
