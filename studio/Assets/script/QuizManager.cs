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
    
    private int numberOfQuestionsToSelect;
    
    private List<QuestionAndAnswer> qnA = new List<QuestionAndAnswer>();
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
    public TextMeshProUGUI questionProgressText;
    
    int totalQuestions = 0;
    // variable stores the total number of questions in the game
    public int score; 
    //keeps track of the player's score.
    
    private List<int> answerIndices = new List<int>();
    //answerIndices is a list of integers used to keep track of the indices of answer options for the current question.
    //It will help with the randomization of answers associated to each question when displayed 
    
    private const int maxAnswerOptions = 4; // Define a constant for maximum answer options
    
    private void Start()
    {
        numberOfQuestionsToSelect = 8;
        LoadQuestionsFromCSV();
        totalQuestions = qnA.Count;
        GoPanel.SetActive(false);
        GenerateQuestion();
    }
    //In the Start method, it initializes the game by setting totalQuestions,
    //hiding GoPanel, and generating the first question using GenerateQuestion.
    
private void LoadQuestionsFromCSV()
{
    try
    {
        string[] csvLines = questionsCSV.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        // Determine the total number of questions in the CSV file
        int totalQuestionsInFile = csvLines.Length - 1; // Subtract 1 for the header row

        // Ensure that numberOfQuestionsToSelect doesn't exceed the total number of questions
        numberOfQuestionsToSelect = Mathf.Min(numberOfQuestionsToSelect, totalQuestionsInFile);

        // Create a list of unique random indices to select questions
        List<int> randomIndices = new List<int>();
        while (randomIndices.Count < numberOfQuestionsToSelect)
        {
            int randomIndex = Random.Range(1, totalQuestionsInFile + 1); // Random index, excluding the header
            if (!randomIndices.Contains(randomIndex))
            {
                randomIndices.Add(randomIndex);
            }
        }

        for (int i = 0; i < numberOfQuestionsToSelect; i++)
        {
            int dataRowIndex = randomIndices[i];
            string[] data = csvLines[dataRowIndex].Split(';'); // Use semicolon as the delimiter

            if (data.Length >= maxAnswerOptions + 2)
            {
                QuestionAndAnswer qa = new QuestionAndAnswer();
                qa.question = data[0];
                List<string> answersList = data.Skip(1).Take(maxAnswerOptions).ToList(); // Always take up to 4 answers
                answersList.RemoveAll(answer => string.IsNullOrWhiteSpace(answer)); // Remove empty answers
                qa.answers = answersList.ToArray();
                int.TryParse(data[data.Length - 1], out qa.correctAnswer);
                qnA.Add(qa);
            }
            else
            {
                Debug.LogWarning("Invalid data in line " + dataRowIndex + ": " + csvLines[dataRowIndex]);
            }
        }
    }
    catch (System.Exception e)
    {
        Debug.LogError("Error loading questions from CSV: " + e.Message);
    }
}
    
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
        
        // Hide the question progress text in the Go Panel
        questionProgressText.gameObject.SetActive(false);
    }
    //GameOver method displays the final score and switches
    //between the game and game over panels.

    public void Correct()
    {
        //when you are right
        score += 1;
        qnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext());

    }

    public void wrong()
    {
        //when you answer wrong
        qnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext());

    }
    
    //These are methods Correct and wrong for handling correct
    //and incorrect answers, respectively.
    //The correct updates the score

    IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(1);
        GenerateQuestion();
    }
    
    //waitForNext coroutine waits for a short period before generating the next question.
    
    
    //THE FOLLOWING METHOD is responsible for setting up a new question for the player to answer during the quiz game
    //This code ensures that each new question is selected randomly, and the order of the answer options is also randomized
    // It adjusts the number of active buttons to match the number of answer options for each question.
    
    // checks if there are questions left in qnA list (which holds questions and answers).
    // If there are questions remaining, the method proceeds to select and set up a new question.
    // If there are no questions left, it logs a message and triggers the game over state.
    
    // Randomly selects a question from your qnA list  by generating a random index within the valid range of questions.
    
    //Next is to  associate the answers to the question
    //We have to ensure that answers to the question are disposed randomly
    //Plus i want that if the answer has 2 possible options are shown only 2, if 3 only 3 etc. (MAX. OF 4 POSSIBLE OPTIONS WAS HYPHOTETIZED BY DEFAULT WHEN BUILDING THE GAME )
    
    //Clear the answerIndices list and populate it with indices that represent the available answer options for the current question.
    //The for loop iterates through the answer options of the current question
    //For each answer option, it adds the index i to the answerIndices list.
    //This means that answerIndices will contain the indices of answer options in their original order.
    
    //This code shuffles the answerIndices list to randomize the order of answer options. It uses the Fisher-Yates shuffle algorithm to achieve this. 
    //    The for loop iterates backward through the list of answerIndices, starting from the last index
    //    (the one before the last element) and moving towards the beginning of the list.
    //    Inside the loop, a random index j is generated using Random.Range(0, i + 1).
    //    This index j represents a position in the list of answer indices.
    //    The code then performs a swap operation: It temporarily stores the value at index i in a variable temp,
    //    then replaces the value at index i with the value at index j,
    //    and finally replaces the value at index j with the temporary value temp.
    
    //The code activates the buttons corresponding to the number of answer options for the current question.
    //It sets the button text and determines which answer is correct 
    //Any extra buttons that are not needed are deactivated.
    
    //If there are no questions left (i.e., qnA.Count becomes zero), the method logs a message ("Out of Questions")
    //and triggers the game over state.
    
    
    void GenerateQuestion()
    {
        if (qnA.Count > 0)
        {
            currentQuestion = Random.Range(0, qnA.Count);

            questionTxt.text = qnA[currentQuestion].question;

            // Populate answerIndices with random indices
            answerIndices.Clear(); 
            for (int i = 0; i < qnA[currentQuestion].answers.Length; i++)
            {
                answerIndices.Add(i);
            }

            
            // Shuffle answerIndices to randomize the order
            for (int i = answerIndices.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                int temp = answerIndices[i];
                answerIndices[i] = answerIndices[j];
                answerIndices[j] = temp;
            }
            
            // Activate buttons based on the number of answer options
            for (int i = 0; i < options.Length; i++)
            {
                if (i < answerIndices.Count)
                {
                    options[i].SetActive(true); //// Activate a button
                    options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;
                    options[i].GetComponent<AnswerScript>().isCorrect = false;
                    options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = qnA[currentQuestion].answers[answerIndices[i]];

                    if (qnA[currentQuestion].correctAnswer == answerIndices[i] + 1)
                    {
                        options[i].GetComponent<AnswerScript>().isCorrect = true;
                    }
                }
                else
                {
                    options[i].SetActive(false); //// Deactivate any extra buttons that are not needed.
                }
            }
            // Update the question progress text
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
