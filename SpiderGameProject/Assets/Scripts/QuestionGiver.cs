using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuestionGiver : MonoBehaviour
{
    [SerializeField]
    private float AssertionRange = 30; // how far the player must be to trigger the question
    public Question question;
   // private bool Active = true;
    private Animator myAnimator;
    private const int rangeAdd = 10;

    public QuestionManager questionManager;
    private Text textQuestion;
    private Text[] textAnswer;
    private CircleCollider2D questionRange;

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

        myAnimator = GetComponentInChildren<Animator>();
        //GetComponentInChildren<CircleCollider2D>().radius = AssertionRange;
        questionRange = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        questionRange.radius = AssertionRange;
        questionRange.isTrigger = true;
        
        textQuestion = questionManager.dialogQuestion; //(Text)GameObject.Find("Question");
        textAnswer = questionManager.dialogAnswers;
    }
    void parseText(int questionIndex)
    {
        string[] pickQuestion;
        pickQuestion = questionManager.importText.text.Split('%');


        string[] parseText;
        parseText = pickQuestion[questionIndex].Split('`');

        int answerIndex = 0;
        for (int i = 0; i < parseText.Length; i++)
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
       
        if (question.questionAnswered)
        {
            StartCoroutine(FinishQuestion());
        
            this.enabled = false;
        }

    }
    void AskQuestion()
    {


        // questionManager.currentQuestion = question;
        playerControl.StopPlayer();

        questionManager.ShowQuestion(question);
    }
    [SerializeField]
    public void LeaveQuestion()
    {
        questionManager.HideQestion();
        playerControl.StartPlayer();
    }

    IEnumerator FinishQuestion()
    {
        yield return new WaitForSeconds(3); // waits 4 seconds then hides quesiton
        myAnimator.SetTrigger("Quit");
        playerControl.StartPlayer();
        GetComponent<CircleCollider2D>().enabled = false;
        questionRange.enabled = false;
        
        // hint.enabled = false;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" == questionRange)
        {
            //  Animator temp = GetComponent<Animator>();
            //    temp.SetTrigger("Turn");
            questionRange.radius = AssertionRange + rangeAdd;
            AskQuestion();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //   Animator temp = GetComponent<Animator>();
            //   temp.SetTrigger("Forward");
            questionRange.radius = AssertionRange - rangeAdd;
            LeaveQuestion();
        }

    }
}
