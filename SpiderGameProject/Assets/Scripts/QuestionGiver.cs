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
    private CircleCollider2D rangeCheck;
    private Animator myAnimator;
    private GameObject player;
    private GameObject questionMark;
    private QuestionManager questionManager;
    private Text textQuestion;
    private Text textAnswer;


    PlayerController playerControl;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerController>();
        questionManager = GameObject.Find("QuestionManager").GetComponent<QuestionManager>();
       // questionMark = GetComponentInChildren<Animatator> //("QuestionMark");
        myAnimator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponentInChildren<CircleCollider2D>().radius = AssertionRange;
        textQuestion = questionManager.dialogQuestion; //(Text)GameObject.Find("Question");
        textAnswer = questionManager.dialogAnswers;
        
  
 

        //questionMark.transform.position = new Vector2(transform.position.x, transform.position.y + 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active)
            enabled = false;
        if(question.questionAnswered)
        {
            StartCoroutine(FinishQuestion());
        }
      
    }
    void AskQuestion()
    {
        
        
       // questionManager.currentQuestion = question;
        playerControl.StopPlayer();
        Debug.Log("Pausing Player");
        questionManager.ShowQuestion(question);
      //  new WaitForSecondsRealtime(4);
     //   FinishQuestion();
    }
    [SerializeField]
    public void AnswerQuestion()
    {

    }

    IEnumerator FinishQuestion()
    {
        yield return new WaitForSeconds(3); // waits 4 seconds then hides quesiton
        myAnimator.SetTrigger("Quit");
        playerControl.StartPlayer();
       GetComponent<CircleCollider2D>().enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AskQuestion();
        }
    }
}


