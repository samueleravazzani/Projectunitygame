
[System.Serializable]

public class QuestionAndAnswer
{
    public string question;
    public string[] answers;
    public int correctAnswer; 
}

//    This is a simple data structure used to represent a single question
//     and its possible answers.
//    It contains fields for the question (a string), an array of answers
//    (string array), and the index of the correctAnswer.