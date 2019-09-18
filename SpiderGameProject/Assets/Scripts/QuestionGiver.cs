using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionGiver : MonoBehaviour
{
    [SerializeField]
    private float AssertionRange = 10; // how far the player must be to trigger the question

    [SerializeField]
    private string Question; // temp quesiton metric will change later

    private bool Active = true;
    private CircleCollider2D rangeCheck;
    private Animator myAnimator;
    private GameObject player;
    private GameObject questionMark;



    PlayerController playerControl;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerController>();
        /*  rangeCheck = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;

          rangeCheck.isTrigger = true;
          rangeCheck.radius = AssertionRange; // makes the collider span the radius of the assertion range.
          rangeCheck.enabled = true;*/
        myAnimator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        // add a question 

        // 


        questionMark = GameObject.FindGameObjectWithTag("Question");
        //questionMark.transform.position = new Vector2(transform.position.x, transform.position.y + 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active)
            enabled = false;
        GetComponent<CircleCollider2D>().radius = AssertionRange;
    }
    void AskQuestion()
    {
        Debug.Log("What is the airspeed velocity of an unladen swallow?");
        playerControl.StopPlayer();
    }
    void FinishQuestion()
    {
        // myAnimator.SetTrigger(Quit);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AskQuestion();
        }
    }
}


