using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class QuizManager : MonoBehaviour
{
    public TextAsset questionsCSV;

    public int numberOfQuestionsToSelect;

    private List<QuestionAndAnswer> qnA = new List<QuestionAndAnswer>();

    public GameObject[] options;
    public int currentQuestion;

    public GameObject Quizpanel;
    public GameObject GoPanel;

    public TextMeshProUGUI questionTxt;
    public TextMeshProUGUI ScoreTxt;
    public TextMeshProUGUI questionProgressText;

    int totalQuestions = 0;
    public int score;

    private List<int> answerIndices = new List<int>();
    private const int maxAnswerOptions = 4;

    public int selectedProblemType; // Default to fire

    public string sceneName;
    
    private void Start()
    {
        // Set the selected problem type (e.g., 1 for fire, 2 for floods, etc.)
        SetSelectedProblemType(selectedProblemType);
        LoadQuestionsFromCSV(selectedProblemType, numberOfQuestionsToSelect);
        totalQuestions = qnA.Count;
        GoPanel.SetActive(false);
        GenerateQuestion();
    }

    private void LoadQuestionsFromCSV(int selectedProblemType, int numberOfQuestionsToSelect)
    {
        try
        {
            string[] csvLines = questionsCSV.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

            List<string> filteredLines = new List<string>();

            for (int i = 1; i < csvLines.Length; i++) // Start from 1 to skip the header
            {
                string[] data = csvLines[i].Split(';');
                int problemType = int.Parse(data[data.Length - 1]); //"ProblemType" is the last column of csv file

                if (problemType == selectedProblemType)
                {
                    filteredLines.Add(csvLines[i]);
                }
            }

            // Shuffle filteredLines to randomize the order
            for (int i = filteredLines.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                string temp = filteredLines[i];
                filteredLines[i] = filteredLines[j];
                filteredLines[j] = temp;
            }

            for (int i = 0; i < numberOfQuestionsToSelect && i < filteredLines.Count; i++)
            {
                string[] data = filteredLines[i].Split(';');

                if (data.Length >= maxAnswerOptions + 1) // Updated for the "ProblemType" column
                {
                    QuestionAndAnswer qa = new QuestionAndAnswer();
                    qa.question = data[0];
                    List<string> answersList = data.Skip(1).Take(maxAnswerOptions).ToList();
                    answersList.RemoveAll(answer => string.IsNullOrWhiteSpace(answer));
                    qa.answers = answersList.ToArray();
                    int.TryParse(data[data.Length - 2], out qa.correctAnswer); // Correct answer is the second-to-last column
                    qa.correctAnswer -= 1; // Subtract 1 to match 0-based indexing
                    qnA.Add(qa);
                }
                else
                {
                    Debug.LogWarning("Invalid data in line " + i + ": " + filteredLines[i]);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error loading questions from CSV: " + e.Message);
        }
    }

    public void SetSelectedProblemType(int problemType)
    {
        selectedProblemType = problemType;
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    void GameOver()
    {
        Quizpanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreTxt.text = score + "/" + totalQuestions;
    }

    public void Correct()
    {
        score += 1;
        qnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext());
    }

    public void wrong()
    {
        qnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext());
    }

    IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(1);
        GenerateQuestion();
    }

    void GenerateQuestion()
    {
        if (qnA.Count > 0)
        {
            currentQuestion = Random.Range(0, qnA.Count);

            questionTxt.text = qnA[currentQuestion].question;

            answerIndices.Clear();
            for (int i = 0; i < qnA[currentQuestion].answers.Length; i++)
            {
                answerIndices.Add(i);
            }

            for (int i = answerIndices.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                int temp = answerIndices[i];
                answerIndices[i] = answerIndices[j];
                answerIndices[j] = temp;
            }

            for (int i = 0; i < options.Length; i++)
            {
                if (i < answerIndices.Count)
                {
                    options[i].SetActive(true);
                    options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;
                    options[i].GetComponent<AnswerScript>().isCorrect = false;
                    options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = qnA[currentQuestion].answers[answerIndices[i]];

                    if (qnA[currentQuestion].correctAnswer == answerIndices[i])
                    {
                        options[i].GetComponent<AnswerScript>().isCorrect = true;
                    }
                }
                else
                {
                    options[i].SetActive(false);
                }
            }

            int currentQuestionNumber = totalQuestions - qnA.Count + 1;
            questionProgressText.text = currentQuestionNumber + "/" + totalQuestions;
        }
        else
        {
            Debug.Log("Out of Questions");
            GameOver();
        }
    }
}
