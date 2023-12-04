using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class QuizManager : MonoBehaviour
{
    public TextAsset questionsCSV; //variable that holds the CSV file containing the quiz questions and answers.
    //in the inspector we attach the file csv to this public variable 
    
    public int numberOfQuestionsToSelect; //how many questions should have our quiz game

    private List<QuestionAndAnswer> qnA = new List<QuestionAndAnswer>(); //objects that stores the loaded questions and answers.
    //rom the other script

    public GameObject[] options;  
    //This represents the number of buttons I can have in my quiz game 
    //by default should be 4 and after in the methods is handled the fact that if there are less
    //answers the button disappears but this variable was needed to fix at maximum 4 the option
    private const int maxAnswerOptions = 4; //RELATED TO BEFORE!
    public int currentQuestion; //integer to keep track of the current question index.
    
    public GameObject Quizpanel;
    public GameObject GoPanel;
    //GameObjects representing the quiz panel and game over panel. Attach in the inspector
    
    public TextMeshProUGUI questionTxt;
    public TextMeshProUGUI ScoreTxt;
    public TextMeshProUGUI questionProgressText;
    // TextMeshProUGUI elements for displaying the question, score, and question progress. Attach in the inspector
    
    int totalQuestions = 0; //integer to store the total number of questions.
    public int score; //An integer to keep track of the player's score.
    public int error; //An integer to keep track of the error's made by the player
    public bool errormade; //Bool variable to identify if an error has been done
    
    private List<int> answerIndices = new List<int>(); //A list of integers to store the indices of answer options.
    
    public int selectedProblemType; //An integer to store the selected problem type (e.g., 1 for fire, 2 for plastics).
    
    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Reference to the audio clip
    public AudioClip myAudioClip;
    
    public Button retryButton; //button that makes possible to repeat the game 
    
    // Make audio clips public to assign them in the Unity inspector
    public AudioClip correctSound;
    public AudioClip wrongSound;
    
    public TextMeshProUGUI retryText; //text that appears when ends the game that you made errors
    public TextMeshProUGUI outroText; //text that appears when you end the game and win
    
    private void Start()
    {
        selectedProblemType= GameManager.instance.problem_now; //Call from the gamemanager to know which problem should regard the questions of the quiz
        LoadQuestionsFromCSV(selectedProblemType, numberOfQuestionsToSelect); //Call the method that load the questions from the CSV file indicating the problem type so that 
        //questions are the ones that regard that problem and the number of question that has to be done 
        totalQuestions = qnA.Count; //count how many question are in the quiz; this variable will be useful after to handle the display on the total of the score and of the questions
        GoPanel.SetActive(false); //deactivate the gameoverpanel so to see only the one of the quiz
        GenerateQuestion(); //call the generation of the question method 
        
        // Get the AudioSource component on this GameObject
        audioSource = GetComponent<AudioSource>();

        // Assign the audio clip to the AudioSource component
        audioSource.clip = myAudioClip;

        // Play the audio
        audioSource.Play();

        errormade = false; //initialize the error bool to false
    }

    private void LoadQuestionsFromCSV(int selectedProblemType, int numberOfQuestionsToSelect)
    {
        try
        {
            string[] csvLines = questionsCSV.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

            List<string> filteredLines = new List<string>();
            //The content of the questionsCSV TextAsset is retrieved using questionsCSV.text. The CSV data is typically a string containing lines of text,
            // where each line represents a question and its associated data.
            // csvLines is an array that contains each line of the CSV file. It's split into lines using the newline character '\n'.
            
            
            for (int i = 1; i < csvLines.Length; i++) // Start from 1 to skip the header
            {
                string[] data = csvLines[i].Split(';');
                int problemType = int.Parse(data[data.Length - 1]); //"ProblemType" is the last column of csv file

                if (problemType == selectedProblemType)
                {
                    filteredLines.Add(csvLines[i]);
                }
            }
            
            //The method iterates through csvLines, starting from the second line (index 1) to skip the header row
            //(in the csv the header row has Question,Answer1,Answer2,Answer3,Answer4,Correctanswer,ProblemType)).
            //Each line is split into an array of data using the semicolon ';' as a delimiter.
            //The code extracts the last element from the data array, that represents the "ProblemType" associated with the question.
            //Questions are filtered based on the value of problemType, which is compared to the selectedProblemType parameter
            //passed to the method. If the problemType matches the selected problem type, the line is added to the filteredLines list.

            // Shuffle filteredLines to randomize the order
            for (int i = filteredLines.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                string temp = filteredLines[i];
                filteredLines[i] = filteredLines[j];
                filteredLines[j] = temp;
            }
            
            //Once the questions are filtered, the code shuffles the order of questions in the filteredLines list.
            //This randomizes the order in which the questions will be presented to the player.
            //The shuffling is done using the Fisher-Yates shuffle algorithm, which randomly swaps elements in the list.

            for (int i = 0; i < numberOfQuestionsToSelect && i < filteredLines.Count; i++)
            {
                string[] data = filteredLines[i].Split(';');
                
                    QuestionAndAnswer qa = new QuestionAndAnswer();
                    qa.question = data[0];
                    List<string> answersList = data.Skip(1).Take(maxAnswerOptions).ToList();
                    answersList.RemoveAll(answer => string.IsNullOrWhiteSpace(answer));
                    qa.answers = answersList.ToArray();
                    int.TryParse(data[data.Length - 2], out qa.correctAnswer); // Correct answer is the second-to-last column
                    qa.correctAnswer -= 1; // Subtract 1 to match 0-based indexing
                    qnA.Add(qa);
            }
            
            //The method then iterates through the shuffled filteredLines list to process the questions and answers.
            //If the line meets the criteria, a new QuestionAndAnswer object (qa) is created. The first element in the data array is assumed to be the question, so it's assigned to qa.question.
            //The remaining elements are assumed to be answer options. They are extracted, removing any empty or whitespace-only answers.
            //The correct answer index is the second-to-last element in the data array.
            //It is parsed into an integer and subtracted by 1 to match the 0-based indexing used in the code.
            //Finally, the qa object is added to the qnA list, which stores the loaded questions and answers
            
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
    //Reloads the current scene to restart the quiz.

    void GameOver()
    {
        Quizpanel.SetActive(false); 
        if (!errormade)//if error wasn't made 
        {
            retryButton.gameObject.SetActive(false); //I shouldn't see the retry button but only give the player the possibility to quit
            outroText.gameObject.SetActive(true); //I should see the outro text
            retryText.gameObject.SetActive(false); //and not see the retry text because is not the case
        }
        else
        {
            retryText.gameObject.SetActive(true); //I should see retry button to give the player also the opportunity to play again
            retryButton.gameObject.SetActive(true); //I should see the text to alert of having done wrong 
            outroText.gameObject.SetActive(false); //Instead I should not see the outro winning text 
        }
        GoPanel.SetActive(true); //Having done the logic above, I can activate the gameover panel
        ScoreTxt.text = score + "/" + totalQuestions;
    }
    //Deactivates the quiz panel and activates the game over panel, displaying the player's score.
    
    public void Correct()
    {
        score += 1;
        qnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext());
    }
    
    public void wrong()
    {
        error += 1; //count the nymber of errors
        if (error == 2)
        {
            errormade = true; //if errors are two so the game should end so I change the bool variable to alert me on that 
            GameOver();
        }
        qnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext());
    }
    //Handle the player's response to a question by updating the score and removing the current question from the list of questions.
    //They also trigger a delay before the next question is generated.
    

    IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(1);
        GenerateQuestion();
    }
    //Implements a delay before generating the next question.

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
    
        //Check If There Are Remaining Questions:
        //It begins by checking if there are any remaining questions in the qnA list. If there are no questions left
        //(i.e., qnA.Count is zero), it means the quiz is over, and it calls the GameOver() method to handle the end of the game.

    //Select a Random Question:
    //If there are remaining questions, it proceeds to select a random question to present to the player.
    //It uses Random.Range(0, qnA.Count) to generate a random index within the range of available questions.

    //Display the Question:
    //It sets the text of the questionTxt TextMeshProUGUI element to display the selected question from the qnA list using the currentQuestion index.

    //Randomize Answer Option Order:
    //To avoid having the answer options appear in the same order each time, the code shuffles the answerIndices list.
    //This list is used to determine the order in which the answer options are displayed to the player.
    //The shuffling is done using the Fisher-Yates shuffle algorithm.

    //Populate Answer Options:
    //It then iterates through the options array, which represents the answer buttons or choices in the game interface.
    //For each answer button, it checks if the button index is within the range of the answerIndices list to ensure that it has a corresponding answer option.
    //If there is an available answer option, it sets the button to be active (visible), sets its background color to its default color,
    //and marks it as not correct (isCorrect is set to false).
    //It also updates the text of the button to display the corresponding answer option from the qnA list, based on the shuffled order of answerIndices.
    //It checks if the current answer option is the correct one by comparing the index in answerIndices to the correctAnswer index stored in the qnA list.
    //If they match, the button is marked as correct (isCorrect is set to true).

    //Update Question Progress:
    //It calculates the current question number by subtracting the number of remaining questions in qnA from the total number of questions.
    //This is used to display the player's progress, e.g., "Question 2/10," in the questionProgressText.

   //Quiz Over Handling:
   //If there are no more questions in the qnA list, the method logs a message to the Unity console ("Out of Questions")
   //and then calls the GameOver() method to handle the end of the game.
   
}
