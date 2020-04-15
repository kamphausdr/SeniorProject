﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This Class manages the camera and allows it to follow the player. 
/// </summary>
public class CameraFollow : MonoBehaviour
{
    private Transform targetTransform;

    [SerializeField]
    private GameObject BackgroundObject = null; // Background keeps the camera from going off screen.
    [SerializeField]
    private GameObject Target = null; // this is the target, typically the player, the camera will be following.
    private float leftBounds, rightBounds;
    public float speed = 2.0f;
   
    Vector3 camPos = new Vector3();
    private SpriteRenderer spriteBounds;

    private void Start()
    {
        targetTransform = Target.transform;
        // Set variables to keep camera within bounds of the background sprite.
        // camera only moves on x, but bounds setup incase we change this
        spriteBounds = BackgroundObject.GetComponentInChildren<SpriteRenderer>();
        float yExtent = Camera.main.orthographicSize;
        float xExtent = yExtent * Screen.width / Screen.height;
        leftBounds = BackgroundObject.transform.position.x;
        rightBounds = spriteBounds.size.x + BackgroundObject.transform.position.x;

    }
    void Update()
    {
        float interpolation = speed * Time.deltaTime;
        camPos = new Vector3(targetTransform.position.x, transform.position.y, transform.position.z);
        camPos.x = Mathf.Lerp(this.transform.position.x, Target.transform.position.x, interpolation);
        camPos.z = transform.position.z;
        this.transform.position = camPos;
    }
}
