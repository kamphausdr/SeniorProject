using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestionManager : MonoBehaviour
{
    public Text dialogQuestion;
    public Text dialogAnswers;
    public Text dialogResponse;
    public Canvas questionCanvas;
    public Animator animatior;
    public Question currentQuestion;
    public float waitTime = 2f; // how long after the question is answered to wait;
    // public string[] Answers;
    // Start is called before the first frame update
    void Start()
    {
    //  HideQestion();  
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowQuestion(Question question)
    {
        Reset();
        currentQuestion = question;
        dialogQuestion.text = question.question;
        dialogAnswers.text = "";
        foreach (string answer in question.answers)
        {
            dialogAnswers.text += answer + System.Environment.NewLine;
        }
        animatior.SetBool("IsOpen", true);
       // questionCanvas.enabled = true;

    }
    public void HideQestion()
    {
      //  questionCanvas.enabled = false;
       animatior.SetBool("IsOpen", false);
    }
    public void AnswerQuestion(int index)
    {
        
        if(currentQuestion.checkAnswer(index))
        {
            dialogResponse.color = Color.green;
            dialogResponse.text = "That's correct Good job!!";
            // calls close question, this is how you run thigns that need a timer in unity
            StartCoroutine(CloseQuestion());
        }
        else
        {
            dialogResponse.color = Color.red;
            dialogResponse.text = "Sorry thats wrong, try again!" +index;
        }
    }
    IEnumerator  CloseQuestion()
    {
        yield return new WaitForSeconds(waitTime); // waits 4 seconds then hides quesiton      
        HideQestion();
        currentQuestion.questionAnswered = true;
    }
    private void Reset()
    {
        dialogResponse.text = "";

        
    }
}
