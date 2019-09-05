﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Defines how fast the player moves, can be adjusted in editor
    [SerializeField] // allows field to be adjusted in unity editor
    private float MovementFactor = 0.1f;
    [SerializeField]
    private float JumpHeight = 1f; // For later implementation of the Jumping

    // allow access to gaming components
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;

    // keeps track of direction player is facing, to allow flipping of sprite as needed
    private bool facingRight;


    // Start is called before the first frame update. Here we perform initialization
    void Start()
    {
        facingRight = true; // Most likely player will be moving to right at the begining.

        // gets the two unity components from the player object in order to utilize it
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Provides the input for the player, designed to work with any device

        MovePlayer(horizontal); // Move the player with our function, at the players input velocity.

        // Handle flipping ( if it is necessary it will occur)
        FlipPlayer(horizontal);

    }
    private void MovePlayer(float horizontal)
    {
        // move the player in the direction by the input at the speed of the player also maintaining his current vertical velocity.
        myRigidbody.velocity = new Vector2(horizontal * MovementFactor, myRigidbody.velocity.y);
        // Set the animators Speed parameter we set to check wether or not the player is moving fast enough to change animation states. We want magnitude not direction, thus Abs.
        myAnimator.SetFloat("Speed", Mathf.Abs(horizontal));
    }
    // flips the player if they are being moved in the opposite direction, if currently facing the correct input direction do nothing
    private void FlipPlayer(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            Vector3 flipScale = transform.localScale; // tracks the players current relative transform

            // change the x axis to the oposite direction
            flipScale.x *= -1;
            // perform the actual transformation
            transform.localScale = flipScale;
        }
    }
    private void Jump(float height)
    {
        // todo: add the code for jumping here.
    }
}
