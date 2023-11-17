using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    public List<string> _impostors= new List<string> {"Pippo"};
    public bool[] alreadyTalk;
    private int correct = 0;
    private int incorrect = 0;
    private static AnswerManager instance;

     private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("find more than one dialogue Manager in the scene");
            }
            instance = this;
         }

     private void Start()
     {
         alreadyTalk= new bool[_impostors.Count];
     }

     public static AnswerManager GetInstance()
     {
         return instance;
     }

     public void Answertracking(string name, int choicemade, int randomindex)
     {
         if (_impostors.Contains(name) && randomindex % 2 == 0) //indice del file JSON pari ci si può fidare
         {
             alreadyTalk[_impostors.IndexOf(name)] = true;
             switch (choicemade)
             {
                 case 0: //risposta positiva, allora aumento il correct
                 {
                     correct++;
                     Debug.Log("indice pari e risposta si" + correct.ToString());
                     break;
                 }
                 case 1:
                 {
                     incorrect++; //risposta negativa aumento l'incorrect
                     Debug.Log("indice pari e risposta no"+ incorrect.ToString());
                     break;
                 }
             }
         }

         if (_impostors.Contains(name) && randomindex % 2 != 0) //indice del file JSON dispari non ci si fida
         {
             switch (choicemade)
             {
                 case 0: //se l'utente da risposta affermativa e quindi si fida incremento l'incorrect
                 {
                     incorrect++;
                     Debug.Log("indice dispari e risposta si"+ incorrect.ToString());
                     break;
                 }
                 case 1: //se l'utente non si fida fa bene e aumento il correct
                 {
                     correct++;
                     Debug.Log("indice dispari e risposta no"+ correct.ToString());
                     break;
                 }
             }
         }

     }

}
