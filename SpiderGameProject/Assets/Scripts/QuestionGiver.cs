using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionGiver : MonoBehaviour
{
    [SerializeField]
    private float AssertionRange = 10; // how far the player must be to trigger the question

    // [SerializeField]
    // private string Question; // temp quesiton metric will change later

    
    public Question question;
    private bool Active = true;

    private Animator myAnimator;
    private GameObject player;
    private GameObject questionMark;
    public QuestionManager questionManager;
    public HintGiver hintPair;
    private Text textQuestion;
    private Text[] textAnswer;
    public TextAsset importText;
    public int questionIndex;
    public HintGiver hint;
    PlayerController playerControl;
    // Start is called before the first frame update
    void Start()
    {
        question = new Question();
        parseText(questionIndex);
        question.hint = hint;
        playerControl = GameObject.Find("Player").GetComponent<PlayerController>();
        questionManager = GameObject.Find("QuestionManager").GetComponent<QuestionManager>();
       // questionMark = GetComponentInChildren<Animatator> //("QuestionMark");
        myAnimator =  GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponentInChildren<CircleCollider2D>().radius = AssertionRange;
        textQuestion = questionManager.dialogQuestion; //(Text)GameObject.Find("Question");
        textAnswer = questionManager.dialogAnswers;
        

 

        //questionMark.transform.position = new Vector2(transform.position.x, transform.position.y + 20);
    }
    void parseText(int questionIndex)
    {
        string[] pickQuestion;
        pickQuestion = importText.text.Split('%');

      //  Debug.Log("Part3:" + pickQuestion[2]);
    
        string[] parseText; 
        parseText = pickQuestion[questionIndex].Split('`');

        int answerIndex = 0;
        for (int i = 0; i <parseText.Length; i++)
        {
            if (parseText[i] == "Q")
            {
                i++;
              //  Debug.Log(parseText[i]);
                question.question = parseText[i];
            }
            if (parseText[i] == "*A")
            {
                question.correctAnswerIndex = answerIndex;
                i++;
                question.answers[answerIndex++] = parseText[i];

            }
            if (parseText[i] == "A")
            {
                i++;
                question.answers[answerIndex++] = parseText[i];
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!Active)
            enabled = false;
        if(question.questionAnswered)
        {
            StartCoroutine(FinishQuestion());
            Active = false;
            this.enabled = false;
        }
      
    }
    void AskQuestion()
    {
        
        
       // questionManager.currentQuestion = question;
        //playerControl.StopPlayer();
        Debug.Log("Pausing Player");
        questionManager.ShowQuestion(question);
      //  new WaitForSecondsRealtime(4);
     //   FinishQuestion();
    }
    [SerializeField]
    public void LeaveQuestion()
    {
        questionManager.HideQestion();
    }

    IEnumerator FinishQuestion()
    {
        yield return new WaitForSeconds(3); // waits 4 seconds then hides quesiton
        myAnimator.SetTrigger("Quit");
      //  playerControl.StartPlayer();
   GetComponent<CircleCollider2D>().enabled = false;
       // hint.enabled = false;
        Debug.Log("collision off");

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
          //  Animator temp = GetComponent<Animator>();
        //    temp.SetTrigger("Turn");
            AskQuestion();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
         //   Animator temp = GetComponent<Animator>();
         //   temp.SetTrigger("Forward");
            LeaveQuestion();
        }
    }
}


