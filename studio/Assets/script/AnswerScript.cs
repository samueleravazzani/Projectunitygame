using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

//This script is attached to the answer choice GameObjects in the scene.

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    //boolean isCorrect to determine if the associated answer is correct.
    public QuizManager quizManager;
    // a reference to the QuizManager to communicate with it.

    public Color startColor;

    private void Start()
    {
        startColor = GetComponent<Image>().color; 
    }
    
    public void Answer()
    {
        if (isCorrect)
        {
            GetComponent<Image>().color = Color.green;
            Debug.Log("Correct Answer");
            quizManager.Correct();
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            Debug.Log("Wrong Answer");
            quizManager.wrong();
        }
    }
    
    //The Answer method is called when the player selects an answer.
    //It checks if the answer is correct, and if so, it calls the Correct method in the QuizManager;
    //otherwise, it calls the wrong method.
}
