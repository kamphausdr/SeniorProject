using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Defines how fast the player moves, can be adjusted in editor
    public float MovementFactor = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Create a 2D Vector to track players position
        Vector2 pos = transform.position;
        float horizontal = Input.GetAxis("Horizontal"); // Provides the input for the player, designed to work with any device
        // move the player by the movement factor, moves in the direction the player is inputing
        pos.x = pos.x + MovementFactor * horizontal * Time.deltaTime; // Delta time ensures game is synced with framerate so cpu speed doesnt effect speed
        transform.position = pos;
    }
}
