using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogeManager : MonoBehaviour
{
   [Header("Dialogue UI")] [SerializeField]
   private GameObject DialoguePanel;

   [SerializeField] private TextMeshProUGUI DialogueText;

   [Header("Choices UI")] [SerializeField]
   private GameObject[] choices;

   private TextMeshProUGUI[] choicesText;

   private Story currentStory;
   public bool dialogueIsPlaying { get; private set; }
   
   private static DialogeManager instance;

   //public GameObject speakingNPC;
   public bool choiceismade = false;
   public int choicemade;

   private void Awake() //creation singleton
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


   private void Start()
   { //set delle variabili
      dialogueIsPlaying = false;
      DialoguePanel.SetActive(false);

      choicesText = new TextMeshProUGUI[choices.Length];
      int index = 0;
      foreach (GameObject choice in choices )
      {
         choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
         index++;
      }
   }

   private void Update()
   {
      if (!dialogueIsPlaying)
      {
         return;
      }

      if (Input.GetKeyDown(KeyCode.Space)) //per cambiare riga del file testo 
      {
         ContinueStory();
      }
   }

   public void EnterDialogueMode(TextAsset inkJSON)
   {
      currentStory = new Story(inkJSON.text);
      dialogueIsPlaying = true;
      DialoguePanel.SetActive(true);

      ContinueStory();
   }
   
   private void ExitDialogueMode()
   {
      dialogueIsPlaying = false;
      DialoguePanel.SetActive(false);
      DialogueText.text = "";
   }

   private void ContinueStory()
   {
      if (currentStory.canContinue)
      {
         DialogueText.text = currentStory.Continue();
         DisplayChoices();
      }
      else
      {
         ExitDialogueMode();
      }

   }

   private void DisplayChoices()
   {
      List<Choice> currentChoices = currentStory.currentChoices;
      if (currentChoices.Count > choices.Length)
      {
         Debug.LogError("More choices were given than the UI can support." +
                        " Number of choices given: "+ currentChoices.Count);
      }

      int index = 0;
      foreach (Choice choice in currentChoices)
      {
         choices[index].gameObject.SetActive(true);
         choicesText[index].text = choice.text;
         index++;
      }

      for (int i = index; i < choices.Length; i++)
      {
         choices[i].gameObject.SetActive(false);
         
      }


      StartCoroutine(SelectFirstChoice());
   }

   private IEnumerator SelectFirstChoice()
   {
      EventSystem.current.SetSelectedGameObject(null);
      yield return new WaitForEndOfFrame();
      EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
   }

   public void MakeChoice(int choiceIndex)
   {
      // currentNPC.GetComponent<NPCScript>().PassChoice(questionIndex, choiceIndex);
      Debug.Log("choice number " + choiceIndex.ToString());
      choiceismade = true;
      choicemade = choiceIndex;
      AnswerManager.GetInstance().Answertracking(DialogeTrigger.GetInstance().name,choicemade,
         DialogeTrigger.GetInstance().randomIndex);
      currentStory.ChooseChoiceIndex(choiceIndex);
   }
   
}
