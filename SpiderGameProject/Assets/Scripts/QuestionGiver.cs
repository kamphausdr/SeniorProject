using System.Collections;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Represents an entity that is able to ask questions to the player - works with question manager
/// </summary>
public class QuestionGiver : MonoBehaviour
{
    [SerializeField]
    private float AssertionRange = 30; // how far the player must be to trigger the question
    public Question question;
    private Animator myAnimator;
    private const int rangeAdd = 10;

    public QuestionManager questionManager; // link to questionManager required to function properly
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

        questionRange = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        questionRange.radius = AssertionRange;
        questionRange.isTrigger = true;
        
        textQuestion = questionManager.dialogQuestion;
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

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" == questionRange)
        {
            questionRange.radius= AssertionRange  + rangeAdd;
            AskQuestion();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            questionRange.radius = AssertionRange;
            LeaveQuestion();
        }

    }
}
