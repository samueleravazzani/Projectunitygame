using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogeManager : MonoBehaviour
{  //Script che gestisce le interazioni con gli NPC
   
   //CANVAS
   [Header("Dialogue UI")] [SerializeField]
   private GameObject DialoguePanel; //Pannello di Dialogo

   [SerializeField] private TextMeshProUGUI DialogueText;//box di testo inserito nel pannello del dialogo

   [Header("Choices UI")] [SerializeField]
   private GameObject[] choices; //bottoni gameobject interactable per effettuare le scelte

   private TextMeshProUGUI[] choicesText; //box di testo che compare sul bottone
   
   //DICHIARAZIONE VARIABILI

   private Story currentStory; //elemento presente nel pacchetto ink.Runtime per gestire il dialogo
   public bool dialogueIsPlaying { get; private set; } //variabile che mi permette di capire se cè un dialogo in corso
   
   private static DialogeManager instance; //variabile per la creazione dell'instace (singleton)
   
   public bool choiceismade = false;//variabile che mi dice se è stata fatta una scelta
   
   public int choicemade;//variabile che mi salva la scelta effettuata
    
   public string nameNPC; //variabile di debug per salvare la variabile nameNPC del Dialogue Trigger
   public int randomIndex; //variabile di debug per salvare la variabile randomIndex del Dialogue Trigger

   //INIZIO CODICE
   
   private void Awake() //Creazione del Singleton
   {
      if (instance != null)
      {
         Debug.LogWarning("find more than one dialogue Manager in the scene");
      }
      instance = this;
   }

   public static DialogeManager GetInstance()
   {
      return instance;
   }

   //START
   private void Start()
   { //Inizializzazione delle variabili
      
      dialogueIsPlaying = false; //all'inizio non c'è nessun dialogo in corso
      DialoguePanel.SetActive(false); //il pannello che mostra i dialoghi è disattivo
      
      //inizializzazione del pannello choice
      choicesText = new TextMeshProUGUI[choices.Length];
      int index = 0;
      foreach (GameObject choice in choices )
      {
         choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
         index++;
      }
   }
   
   //UPDATE 

   private void Update()
   {
      if (!dialogueIsPlaying)
      {
         return; //se non c'è un dialogo in corso non bisogna gestire nulla
      }

      if (Input.GetKeyDown(KeyCode.Space)) 
      {  //Se viene premuta la barra spaziatrice allora viene chiamata la funzione per proseguire il dialogo.
         ContinueStory();
      }
   }
   
   //FUNZIONI
   
   
   //FUNZIONE PER ENTRARE NEL DIALOGO

   public void EnterDialogueMode(TextAsset inkJSON)  
   {  //Prende in ingresso il file ink attaccato al trigger dell'NPC; questa funzione viene chiamata
      //all'interno del Dialogue Trigger
      
      currentStory = new Story(inkJSON.text); //inizializzazione del dialogo (apre il file inkJSON)
      
      dialogueIsPlaying = true; //avviso che c'è un dialogo in corso
      DialoguePanel.SetActive(true); //attivo il pannello per mostrare il dialogo

      ContinueStory();
   }
   
   //FUNZIONE CHE FINISCE IL DIALOGO
   
   private void ExitDialogueMode()
   {
      dialogueIsPlaying = false; //avviso che il dialogo è finito
      DialoguePanel.SetActive(false); //disattivo il pannello che mostra il dialogo
      DialogueText.text = ""; //per buona convenzione si azzera pure il testo del pannello
   }
   
   //FUNZIONE CONTINUE STORY

   private void ContinueStory()
   {  //Se la storia possiede un altro paragrafo, allora premendo la barra spaziatrice(input), allora
      //il dialogo passa al paragrafo successivo
      
      if (currentStory.canContinue)
      {
         DialogueText.text = currentStory.Continue(); //aggiornamento del pannello di dialogo col paragrafo successivo
         DisplayChoices(); //se in quel paragrafo sono presenti delle scelte, allora vengono mostrate con questa funzione
      }
      else
      {
         ExitDialogueMode(); //se il dialogo non presenta altri paragrafi da mostrare allora si esce dal dialogo
      }

   }
   
   
   //FUNZIONE PER MOSTRARE LE SCELTE

   private void DisplayChoices()
   {
      List<Choice> currentChoices = currentStory.currentChoices; //Salva le possibili scelte del dialogo in una lista
      
      if (currentChoices.Count > choices.Length) //controllo con la lunghezza del pannello delle scelte dei bottoni
      {
         Debug.LogError("More choices were given than the UI can support." +
                        " Number of choices given: "+ currentChoices.Count);
      }
      
      //nel caso non ci siano problemi, proseguo a mostrare le scelte.
      
      int index = 0; //indice che scorre le scelte
      
      foreach (Choice choice in currentChoices)
      {
         choices[index].gameObject.SetActive(true); //attivo il pannello relativo alla scelta
         choicesText[index].text = choice.text; //mostro la scelta presente sul bottone
         index++;
      }
      
      //se possiedo più bottoni delle scelte contenute nel dialogo, allora disattivo quei bottoni
      
      for (int i = index; i < choices.Length; i++)
      {
         choices[i].gameObject.SetActive(false);
         
      }

      //Per implementare una scelta bisogna di Default selezionare prima la prima, e poi permettere
      //all'utente di scorrere tra le varie scelte e successivamente selezionarne una.
      
      StartCoroutine(SelectFirstChoice());
   }
   

   //FUNZIONE PER SELEZIONARE LA PRIMA SCELTA
   private IEnumerator SelectFirstChoice()
   {
      EventSystem.current.SetSelectedGameObject(null);
      yield return new WaitForEndOfFrame();
      EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
   }
   
   //FUNZIONE PER EFFETTUARE LA SCELTA. QUESTA FUNZIONE è LEGATA AL BOTTONE!
   //NON VIENE CHIAMATA NELLO SCRIPT!!

   public void MakeChoice(int choiceIndex)
   {
      //Alla funzione viene passato l'indice della scelta effettuata
      
      Debug.Log("choice number " + choiceIndex.ToString());
      
      choiceismade = true; //variabile che mi dice che è stata effettuata una scelta
      
      choicemade = choiceIndex; //variabile che salva l'indice della scelta effettuata
      
      //salvataggio delle variabili di debug
      nameNPC = DialogeTrigger.GetInstance().nameNPC.ToString();
      randomIndex = DialogeTrigger.GetInstance().randomIndex;
      
      Debug.Log("nome salvato:"+nameNPC.ToString());
      Debug.Log("randomIndex salvato:"+randomIndex.ToString());
      
      
      //Una volta effettuata la scelta viene chiamata la funzione presente nell'AnswerManager che mi permette
      //di gestire il gioco vero o falso, tenendo traccia della scelta che ha effettuato il player, oltre al file
      //a cui ha risposto e all'NPC con cui il player ha interagito.
      
      AnswerManager.GetInstance().Answertracking(DialogeTrigger.GetInstance().nameNPC,choicemade,
         DialogeTrigger.GetInstance().randomIndex);
      
      
      currentStory.ChooseChoiceIndex(choiceIndex); //fa andare avanti il dialogo in base alla scelta effettuata
   }
   
}
