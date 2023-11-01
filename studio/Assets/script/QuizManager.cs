using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswer> qnA;
    //It stores a list of QuestionAndAnswer objects (qnA)
    //to represent the questions and answers for the quiz.
    public GameObject[] options;
    //an array of GameObject options, which are used to
    //display answer choices in the game.
    public int currentQuestion;
    //variable keeps track of the current question being displayed.
    
    //two game panels
    public GameObject Quizpanel;
    //panel where questions are displayed
    public GameObject GoPanel;
    //panel shown when the game is over.
    
    public TextMeshProUGUI questionTxt;
    //for displaying the question text
    public TextMeshProUGUI ScoreTxt;
    //for displaying the player's score.
    
    int totalQuestions = 0;
    // variable stores the total number of questions in the game
    public int score; 
    //keeps track of the player's score.
    
    private void Start()
    {
        totalQuestions = qnA.Count;
        GoPanel.SetActive(false);
        GenerateQuestion();
    }
    //In the Start method, it initializes the game by setting totalQuestions,
    //hiding GoPanel, and generating the first question using GenerateQuestion.
    

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //retry method to restart the game by reloading the current scene.
    

    void GameOver()
    {
        Quizpanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreTxt.text =  score + "/" + totalQuestions;
    }
    //GameOver method displays the final score and switches
    //between the game and game over panels.

    public void Correct()
    {
        //when you are right
        score += 1;
        qnA.RemoveAt(currentQuestion);
        GenerateQuestion();
    }

    public void wrong()
    {
        //when you answer wrong
        qnA.RemoveAt(currentQuestion);
        GenerateQuestion();
    }
    
    //There are methods Correct and wrong for handling correct
    //and incorrect answers, respectively.

    IEnumerator waitForNext()
    {
        yield return new WaitForSeconds(1);
        GenerateQuestion();
    }
    
    //waitForNext coroutine waits for a short period before generating the next question.

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false; 
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = qnA[currentQuestion].answers[i];

            if (qnA[currentQuestion].correctAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true; 
            }
        }
    }
    
    //SetAnswers method sets the answer choices for the current question
    //and determines which one is the correct answer

    void GenerateQuestion()
    {
        if (qnA.Count > 0)
        {
            currentQuestion = Random.Range(0, qnA.Count);

            questionTxt.text = qnA[currentQuestion].question;
            SetAnswers();
        }
        else
        {
            Debug.Log("Out of Questions");
            GameOver();
        }
    }
    //GenerateQuestion method selects a random question from the list and updates the question text and answer choices.
    //If no questions are left, it calls GameOver.
    
}
