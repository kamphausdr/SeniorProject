using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintGiver : MonoBehaviour
{
    [SerializeField]
    private float AssertionRange = 10; // how far the player must be to trigger the question
    private bool Active = true;
    [SerializeField]
    public Animator animatiorCanvas;
    [SerializeField]
    private string Hint = "";
    [SerializeField]

    public Text hintText;
  
    private Animator myAnimator;
    private GameObject player;
    PlayerController playerControl;

    // Start is called before the first frame update
    void Start()
    {   
        playerControl = GameObject.Find("Player").GetComponent<PlayerController>();

        // questionMark = GetComponentInChildren<Animatator> //("QuestionMark");
        myAnimator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponentInChildren<CircleCollider2D>().radius = AssertionRange;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (!Active)
            enabled = false;
    }
    public void shutDown()
    {
        
    }
    public void showHintMarker()
    {
        myAnimator.SetTrigger("Show");
    }
    public void hideHint()
    {
        //myAnimator.SetTrigger("hide");
    }
    void ShowHint()
    {
        hintText.text = Hint;
        animatiorCanvas.SetBool("IsOpen", true);
    }
    [SerializeField]
    public void HideHint()
    {
        animatiorCanvas.SetBool("IsOpen", false);     
        // questionManager.HideQestion();
    }


    private void  OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ShowHint();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            HideHint();
           // Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, false);
        }

    }
}


