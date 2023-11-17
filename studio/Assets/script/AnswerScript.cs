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
    
    // Make AudioSource variables public to assign them in the Unity inspector
    public AudioSource correctAudioSource;
    public AudioSource wrongAudioSource;

    private void Start()
    {
        startColor = GetComponent<Image>().color; 
        
        // Assign AudioSource components for correct and wrong sounds
        correctAudioSource = quizManager.gameObject.AddComponent<AudioSource>();
        correctAudioSource.clip = quizManager.correctSound;

        wrongAudioSource = quizManager.gameObject.AddComponent<AudioSource>();
        wrongAudioSource.clip = quizManager.wrongSound;
    }
    //acquire the beginning color of the button (yellow background) 
    
    public void Answer()
    {
        if (isCorrect)
        {
            GetComponent<Image>().color = Color.green;
            Debug.Log("Correct Answer");
            quizManager.Correct();
            correctAudioSource.Play(); // Play correct sound
        }
        else
        {
            // Mark the selected answer in red
            GetComponent<Image>().color = Color.red;

            // Mark the correct answer in green
            MarkCorrectAnswerGreen();

            Debug.Log("Wrong Answer");
            quizManager.wrong();
            wrongAudioSource.Play(); // Play wrong sound
        }
    }
    
    //The Answer method is called when the player selects an answer.
    //It checks if the answer is correct, and if so, it calls the Correct method in the QuizManager;
    //otherwise, it calls the wrong method.
    //It also changes color to red and green according to wrong or correct to visualize it!
    //In the case of wrong answer is collect the MarkCorrectAnswerGreen method 
    //so that when the answer is wrong is shown also which one was correct 

    private void MarkCorrectAnswerGreen()
    {
        // Iterate through the answer options to find and mark the correct answer in green
        for (int i = 0; i < quizManager.options.Length; i++)
        {
            if (quizManager.options[i].GetComponent<AnswerScript>().isCorrect)
            {
                quizManager.options[i].GetComponent<Image>().color = Color.green;
                break;  // Exit the loop after marking the correct answer
            }
        }
    }
    

}
