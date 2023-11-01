using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswer> qnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject Quizpanel;
    public GameObject GoPanel;

    public TextMeshProUGUI questionTxt;
    public TextMeshProUGUI ScoreTxt;

    int totalQuestions = 0;
    public int score; 
    
    private void Start()
    {
        totalQuestions = qnA.Count;
        GoPanel.SetActive(false);
        GenerateQuestion();
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GameOver()
    {
        Quizpanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreTxt.text =  score + "/" + totalQuestions;
    }

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

    IEnumerator waitForNext()
    {
        yield return new WaitForSeconds(1);
        GenerateQuestion();
    }

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
}
