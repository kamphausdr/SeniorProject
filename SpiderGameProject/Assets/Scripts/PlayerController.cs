using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Defines how fast the player moves, can be adjusted in editor
    [SerializeField] // allows field to be adjusted in unity editor
    private float MovementFactor = 0.1f;
    [SerializeField]
    // Defines Player Jump Height ratio based off the jump height.
    private float JumpFactor = 1f;

    private const float jumpHeight = 10f; // Gives a nonchangable jump height.
    // allow access to gaming components
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private Collider2D myCollider;

    private bool Jumping = false; // Tracks if player is currently jumping to prevent double jump.

    // keeps track of direction player is facing, to allow flipping of sprite as needed
    private bool facingRight;
    private bool inColision = false;

   
    // Start is called before the first frame update. Here we perform initialization
    void Start()
    {
        facingRight = true; // Most likely player will be moving to right at the begining.

        // gets the two unity components from the player object in order to utilize it
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponentInChildren<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Provides the input for the player, designed to work with any device
        if(Jumping) // check to see if player is jumping
        {
            if(myRigidbody.velocity.y == 0)
            {
                Jumping = false;
                myAnimator.SetBool("Jumping", false);
               // myCollider.sharedMaterial.friction = 0.4f;
            }
        }
        MovePlayer(horizontal); // Move the player with our function, at the players input velocity.
        if (Input.GetKeyUp(KeyCode.Space) && !Jumping )
        {
            Jump(JumpFactor * jumpHeight); // causes the player to jump at his set jump factor times the jump ratio set by the editor
        }
        // Handle flipping ( if it is necessary it will occur)
        FlipPlayer(horizontal);

    }
    private void MovePlayer(float horizontal)
    {
        // move the player in the direction by the input at the speed of the player also maintaining his current vertical velocity.

        if (!(inColision && Jumping))
        {
         
                 
                // myCollider.sharedMaterial.friction = 0;
                myRigidbody.velocity = new Vector2(horizontal * MovementFactor, myRigidbody.velocity.y);
            myAnimator.SetFloat("Speed", Mathf.Abs(horizontal));
        }
        else
        {
            Debug.Log("In collision and jumping stop movement");
        }
      
        // Set the animators Speed parameter we set to check wether or not the player is moving fast enough to change animation states. We want magnitude not direction, thus Abs.
   
    }
    // flips the player if they are being moved in the opposite direction, if currently facing the correct input direction do nothing
    private void FlipPlayer(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            Vector3 flipScale = transform.localScale; // tracks the players current relative transform

            // change the x axis to the oposite direction
            flipScale.x *= -1;
            facingRight =! facingRight;
            // perform the actual transformation
            transform.localScale = flipScale;
        }
    }
    // Handles the player jumping, specifies the height so that outside mechanism can potentially jump at other heights
    private void Jump(float height)
    {
        // Set a flag to indicate player is jumping, so that he cannot jump an infinite amount of times
        Jumping = true;
        float vertVel = myRigidbody.velocity.x;
        // Set a flag for the animator used to transition the animation to jumping
        myAnimator.SetBool("Jumping", true);
        // applies the vertical velocity in order to make the player jump. Maintains his current horizontal velocity
        if (inColision)
            vertVel = 0;
        myRigidbody.velocity = new Vector2(vertVel, height);
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        inColision = true;
   
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        inColision = false;
    }
}
