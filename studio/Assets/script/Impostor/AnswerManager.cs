using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AnswerManager : MonoBehaviour
{   //SCRIPT CHE MI PERMETTE DI GESTIRE LE RISPOSTE NELLA SCENA IMPOSTOR
    
    public List<string> impostors= new List<string>(){"Gigetto","Pietro","Jack","Lupetto","Gina","Rocky",
        "Bianca","Guardia"}; //Lista di "impostori" presenti nella scena con cui interagire e
    //giocare selezionando le risposte tra vero e falso
    
    public bool[] alreadyTalk; //vettore di buleani di lunghezza pari alla lista degli impostori che mi 
    //permette di tener traccia degli NPC con cui il player ha interagito
    
    private int correct = 0;//variabile che mi permette di tener traccia delle risposte corrette
    
    private int incorrect = 0;//variabile che mi permette di tener traccia delle risposte errate
    
    private static AnswerManager instance; //creazione dell'istance per il singleton
    
    [Header("Ink JSON alreadyTalk")] [SerializeField] private TextAsset inkJSON;
    //File inkJSON contenuto nell'AnswerManager contenente delle risposte nel caso in cui il player
    //abbia già interagito con quell'NPC
    
    [Header("Castle Door")] [SerializeField]
    private GameObject CastleDoor;
    
    //CREAZIONE DEL SINGLETON

     private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("find more than one dialogue Manager in the scene");
            }
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
         }
     
     public static AnswerManager GetInstance()
     {
         return instance;
     }
     
     
     //START

     private void Start()
     {
         alreadyTalk= new bool[impostors.Count]; //inizializzazione del vettore di buleani
         //deve avere la stessa lunghezza della lista di impostori
     }

     //FUNZIONE CHE MI PERMETTE DI GESTIRE LE RISPOSTE

     public void Answertracking(string name, int choicemade, int randomindex)
     {
         //Funzione chiamata nel DialogueManager per tenere traccia delle risposte nella scena impostor
         
         //debugging
         Debug.Log("sono nella funzione");
         Debug.Log("nome passato : "+ name.ToString());
         Debug.Log("scelta fatta: "+choicemade.ToString());
         Debug.Log("indice passato: "+randomindex.ToString());
         
         if (name == "Pippo")
         {
             CastleDoor.SetActive(false);
         }
         
         
         
         if (impostors.Contains(name) && randomindex % 2 == 0) 
         {  
             //Se il nome passato dalla funzione è contenuto nella lista degli impostori e se il file inkJSON
             //aperto, contenuto nel vettore dei dialoghi presente nel trigger dell'NPC, ha indice pari allora...
             
             Debug.Log("sono nel if della funzione");//debugging
             
             alreadyTalk[impostors.IndexOf(name)] = true; //il booleano all'indice del nome dell'NPC passato va a true
             //in questo modo posso dire di aver parlato con quell'NPC 
             
             switch (choicemade) //in base all'indice della risposta data 0 True e 1 false, avremmo queste alternative
             {
                 case 0: //risposta positiva, allora aumento il correct
                 {
                     correct++;
                     Debug.Log("indice pari e risposta si" + correct.ToString());
                     break;
                 }
                 case 1://risposta negativa aumento l'incorrect
                 {
                     incorrect++; 
                     Debug.Log("indice pari e risposta no" + incorrect.ToString());
                     break;
                 }
             }
         }

         if (impostors.Contains(name) && randomindex % 2 != 0) 
         {
             //Se il nome passato dalla funzione è contenuto nella lista degli impostori e se il file inkJSON
             //aperto, contenuto nel vettore dei dialoghi presente nel trigger dell'NPC, ha indice pari allora...
             
             switch (choicemade)
             {
                 case 0: //risposta positiva, allora aumento l'incorrect
                 {
                     incorrect++;
                     Debug.Log("indice dispari e risposta si" + incorrect.ToString());
                     break;
                 }
                 case 1: //risposta negativa, allora aumento il correct
                 {
                     correct++;
                     Debug.Log("indice dispari e risposta no" + correct.ToString());
                     break;
                 }
             }
         }

     }

     //FUNZIONE CHE CHIAMA IL DIALOGUE TRIGGER NEL CASO IN CUI HO GIà PARLATO CON UN NPC PRESENTE NELLA
     //LISTA DEGLI IMPOSTORI
     public void AlreadyTalk()
     {
         DialogeManager.GetInstance().EnterDialogueMode(inkJSON);
         //viene passato il dialogo contenente le possibili risposte nel caso abbia già parlato con
         //quell'NPC
     }
     
     
}
