using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Represents a Question that can be asked by a question giver and is handled with question manager as well.
/// </summary>
public class Question
{
    // The following are special unity comments that allow the editor to format paramaters in desired manner :
  //  [TextArea(2, 10)]
    public string question;
   // [TextArea(2, 10)]
    public string[] answers;

    public int correctAnswerIndex;
    public HintGiver hint;
    public bool questionAnswered;
    // Default constructor set everything blank
    public Question()
    {
        answers = new string[4];
        question = "";
      
        answers[0] = "";
        answers[1] = "";
        answers[2] = "";
        answers[3] = "";
        
        correctAnswerIndex = 0;
        questionAnswered = false;
    }
    // checks that the given answer matches the correct answer index.
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
