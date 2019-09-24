using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    // Start is called before the first frame update
    [TextArea(2, 10)]
    public string question;
    [TextArea(2, 10)]
    public string[] answers;

    public int correctAnswerIndex;

    public bool questionAnswered;

    public bool checkAnswer(int answerIndex)
    {
        if (answerIndex == correctAnswerIndex)
        { 
            questionAnswered = true;
            return true;
        }
        else
        {
            questionAnswered = false;
            return false;

        }
    }
}
