using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestionManager : MonoBehaviour
{
    /// <summary>
    /// The corresponding question dialog text box. Required to change the value to match the question data.
    /// </summary>
    public Text dialogQuestion;
    /// <summary>
    /// Correspondin question dialog answer text boxes. Allows changing of the answers to match answer data.
    /// </summary>
    public Text[] dialogAnswers;
    /// <summary>
    /// Corresponding question dialog response text box. Allows response to the provided answer.
    /// </summary>
    public Text dialogResponse;
    /// <summary>
    /// Specifies the question canvas object, set this to the canvas in the scene that will display the questions.
    /// </summary>
    public Canvas questionCanvas;
    /// <summary>
    /// Specifies the animator of the question canvas, allows the manipulation of animations of the dialog entry and exit.
    /// </summary>
    public Animator animatior;
    /// <summary>
    /// Specifies the question data the canvas is currently selected to. The canvas will display questions and answers to whatever question object is selected.
    /// </summary>
    public Question currentQuestion;
    /// <summary>
    /// How much time after the question is answered until it goes away and allows access.
    /// </summary>
    public TextAsset importText;
    public float waitTime = 2f; // how long after the question is answered to wait;
    /// <summary>
    /// Tracks number of times the question was answered incorrectly, used for score tracking and hint triggering.
    /// </summary>
    private int timesWrong = 0;
    /// <summary>
    /// Takes the provided question and sets all the text boxes in the question dialog to match the question. Then sets the animation flag for the dialog to open in order to display the question
    /// </summary>
    /// <param name="question"></param>
    private LevelManager levelManager;
    public void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
    public void ShowQuestion(Question question)
    {
        //Only change all the data selected if a different question has changed. No need to reassign the variables if it didn't change.
        if (currentQuestion != question)
        {
            // Clear out any existing data 
            Reset();

            // Now set all of the data to the question passed.
            currentQuestion = question;
            dialogQuestion.text = question.question;

            // cycle through all the answers and set the text appropriately
            int index = 0; // use an index tracker for the array
            foreach (string answer in question.answers)
            {
                dialogAnswers[index].text = answer;
                index++;
            }
        }
        // Set the flag for the animator to trigger the open question animation. This will actually make the dialog appear.
        animatior.SetBool("IsOpen", true);
       }
    /// <summary>
    /// Sets the animation flag for the dialog to open in order to display the question
    /// </summary>
    public void HideQestion()
    {
        // Set the flag for the animator to trigger the close question animation. This will actually make the dialog disapear.
        animatior.SetBool("IsOpen", false);
    }
   /// <summary>
   /// Attempts to answer the question based with the given answer index. Checks the answer and provides the feedback, if answered correctly will clear the question, and path.
   /// </summary>
   /// <param name="index"></param>
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
    /// <summary>
    /// Finishes the question, and make it go away.
    /// </summary>
    /// <returns></returns>
    IEnumerator  CloseQuestion() // Required to be IEnumerator in order to utilize Wait function (built in unity requirement)
    {
        yield return new WaitForSeconds(waitTime); // waits 4 seconds then hides quesiton. Syntax based on unity requirements for waiting function      
        HideQestion();
        currentQuestion.questionAnswered = true;
        levelManager.AddPoints(timesWrong);
    }
    /// <summary>
    /// Clears the response and timesWrong tracker.
    /// </summary>
    private void Reset()
    {
        // Clear the response, because it doesn't get set directly by the question when it changes.
        dialogResponse.text = "";
        timesWrong = 0;        
    }
}
