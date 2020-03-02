using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestionManager : MonoBehaviour
{
    public Text dialogQuestion;
    public Text[] dialogAnswers;
    public Text dialogResponse;
    public Canvas questionCanvas;
    public Animator animatior;
    public Question currentQuestion;
 
    public float waitTime = 2f; // how long after the question is answered to wait;
    private int timesWrong = 0;
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
        //  dialogAnswers.text = "";
        int i = 0;
        foreach (string answer in question.answers)
        {
           dialogAnswers[i].text = answer;
            i++;
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
            timesWrong++;
            if(timesWrong <2)
            dialogResponse.text = "Sorry thats wrong, try again!" ;
            else
            {
                dialogResponse.text = "You seem to be having trouble with this question, try looking around for help.";
                if (currentQuestion.hint != null)
                {
                    currentQuestion.hint.showHintMarker();
                }
            }
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
