using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    //boolean isCorrect to determine if the associated answer is correct.
    public QuizManager quizManager;
    // a reference to the QuizManager to communicate with it.

    public Color startColor;
    //reference to capture the object of the background. Used for buttons to switch from yellow background 
    //to green when the question is correct and red when wrong

    private void Start()
    {
        startColor = GetComponent<Image>().color; 
    }
    //acquire the beginning color of the button (yellow background) 
    
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
    //It also changes color to red and green according to wrong or correct to visualize it!
}
