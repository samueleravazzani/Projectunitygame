using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestionnaireManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;

    public TextAsset csvFile; // Attach the CSV file directly in the editor

    private List<string[]> questionsList; // List to store questions and answers from CSV
    private int currentQuestionIndex = -1; // Index to keep track of current question

    private string anxiety, LITERACY, climate; // Variables to store answers

    public string sceneName; //A string variable to specify the name of the scene to load after the quiz ends.
    
    void Start()
    {
        if (questionText == null)
            Debug.LogError("questionText is not assigned!");

        if (answerButtons == null || answerButtons.Length == 0)
            Debug.LogError("answerButtons are not assigned!");

        LoadQuestionsFromCSV();
        DisplayNextQuestion();
    }

    void LoadQuestionsFromCSV()
    {
        if (csvFile != null)
        {
            string[] data = csvFile.text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            questionsList = new List<string[]>();

            foreach (string line in data)
            {
                string[] row = line.Split(';');
                questionsList.Add(row);
            }
        }
        else
        {
            Debug.LogError("CSV file is not attached!");
        }
    }

    public void DisplayNextQuestion()
    {
        currentQuestionIndex++;

        if (questionsList == null)
        {
            Debug.LogError("Questions list is null!");
            return;
        }

        if (currentQuestionIndex < questionsList.Count)
        {
            if (questionText == null)
            {
                Debug.LogError("Question Text component is null!");
                return;
            }

            questionText.text = questionsList[currentQuestionIndex][0];

            if (answerButtons == null || answerButtons.Length == 0)
            {
                Debug.LogError("Answer buttons are null or empty!");
                return;
            }

            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (answerButtons[i] == null)
                {
                    Debug.LogError("Answer button " + i + " is null!");
                    return;
                }

                if (answerButtons[i].GetComponentInChildren<TextMeshProUGUI>() == null)
                {
                    Debug.LogError("Text component in Answer button " + i + " is null!");
                    return;
                }

                int answerIndex = i + 1;
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = questionsList[currentQuestionIndex][answerIndex];
            }
        }
        else
        {
            // Quiz completed, do something (e.g., save answers or move to the next scene)
            Debug.Log("Quiz completed!");
            Debug.Log("Anxiety: " + anxiety);
            Debug.Log("LITERACY: " + LITERACY);
            Debug.Log("Climate: " + climate);
            SceneManager.LoadScene(sceneName);
        }
    }


    public void SetAnswer(int answerIndex)
    {
        switch (currentQuestionIndex)
        {
            case 0:
                anxiety = questionsList[currentQuestionIndex][answerIndex];
                break;
            case 1:
                LITERACY = questionsList[currentQuestionIndex][answerIndex];
                break;
            case 2:
                climate = questionsList[currentQuestionIndex][answerIndex];
                break;
            default:
                break;
        }

        DisplayNextQuestion();
    }
}
