using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class DialogeTrigger : MonoBehaviour //script attaccato al trigger dell'NPC
{ 
    [Header("Visual Cue")] [SerializeField]
    private GameObject visualCue;//visualCue che si trova sopra la testa dell'NPC

    [Header("Ink JSON")] [SerializeField] private TextAsset[] inkJSON;
    //vettore di inkJason contenente i vari dialoghi posseduti da quell'NPC
    
    private bool playerInRange; //mi dice se è il player che collide con l'NPC
    
    public string nameNPC; //nome dell'NPC che corrisponde al suo TAG nel trigger
    
    public int randomIndex;//variabile che sceglie il file inkJSON da leggere
    
    private static DialogeTrigger instance;
    
    
    //
    
    private void Update()
    {
        if (playerInRange && !DialogeManager.GetInstance().dialogueIsPlaying)
        {//controllo se il player collide con l'NPC e che non ci sia già un dialogo in corso.
            
            visualCue.SetActive(true); //attivazione del cubo sopra l'NPC
            
            if (Input.GetKeyDown(KeyCode.Return)) //comando di interazione con NPC, bottone invio.
            {
                nameNPC = gameObject.tag;//salvo il nome dell'NPC con cui ho interagito
                Debug.Log("sono " + nameNPC.ToString());
                if (nameNPC == "Bever")
                {
                    DialogueBever.GetInstance().DialogueBeverManager(inkJSON);
                }
                
                randomIndex = UnityEngine.Random.Range(0, inkJSON.Length); //genero il numero per scegliere il Dialogo
                //il numero è compreso tra 0 e la lunghezza del vettore dei dialoghi
                
                DialogeManager.GetInstance().PlayerInteracted(this);

                //Verifico che se l'NPC fa parte degli impostori e se ho già interagito, allora non ci posso parlare ancora
               if (AnswerManager.GetInstance().impostors.Contains(nameNPC) &&
                    AnswerManager.GetInstance().alreadyTalk[AnswerManager.GetInstance().impostors.IndexOf(nameNPC)])
               {
                    AnswerManager.GetInstance().AlreadyTalk();//funzione che fa comparire il messaggio
                    Debug.Log("hai già parlato con questo NPC");
                }
                //Verifico che se invece l'NPC non fa parte della lista, oppure se fa parte della lista e non ci ho ancora
                //parlato, allora posso far partire il dialogo scelto dal randomIndex
                if ((!AnswerManager.GetInstance().impostors.Contains(nameNPC)||
                    (AnswerManager.GetInstance().impostors.Contains(nameNPC) &&
                     !AnswerManager.GetInstance().alreadyTalk[AnswerManager.GetInstance().impostors.IndexOf(nameNPC)]))
                    && nameNPC !="Bever")
                {
                    DialogeManager.GetInstance().EnterDialogueMode(inkJSON[randomIndex]);
                   
                }
            }
        }
        else
        {
            visualCue.SetActive(false); //disattivo il cubo se l'NPC non è nei paraggi
        }

    }
    
    
    //Funzione che mi mette a true la condizione della collisione del player con l'NPC

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    //Funzione che mi mette a false la condizione della collisione del player con l'NPC
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
        
    }
}
