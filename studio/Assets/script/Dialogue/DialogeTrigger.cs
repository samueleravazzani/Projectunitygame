using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogeTrigger : MonoBehaviour
{ //script attaccato al trigger dell'NPC
    [Header("Visual Cue")] [SerializeField]
    private GameObject visualCue;

    [Header("Ink JSON")] [SerializeField] private TextAsset[] inkJSON;

    private bool playerInRange;
   // private string name
    public string name;
    private static DialogeTrigger instance;
    public int randomIndex;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("find more than one dialogue Manager in the scene");
        }
        instance = this;
        
        playerInRange = false;
        visualCue.SetActive(false);
        
    }
    
    public static DialogeTrigger GetInstance()
    {
        return instance;
    }
    
    private void Update()
    {
        if (playerInRange && !DialogeManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                name = gameObject.tag;
                Debug.Log("sono " + name.ToString());
                //check della lista degli impostori???
                randomIndex = UnityEngine.Random.Range(0, inkJSON.Length);


               if (AnswerManager.GetInstance()._impostors.Contains(name) &&
                    AnswerManager.GetInstance().alreadyTalk[AnswerManager.GetInstance()._impostors.IndexOf(name)])
                {
                    Debug.Log("hai già parlato con questo NPC");
                }
               
                if (!AnswerManager.GetInstance()._impostors.Contains(name) ||
                    (AnswerManager.GetInstance()._impostors.Contains(name) &&
                     !AnswerManager.GetInstance().alreadyTalk[AnswerManager.GetInstance()._impostors.IndexOf(name)]))
                {
                    DialogeManager.GetInstance().EnterDialogueMode(inkJSON[randomIndex]);
                }
                //DialogeManager.GetInstance().EnterDialogueMode(inkJSON[randomIndex]);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }

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
