using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform targetTransform;

    [SerializeField]
    private GameObject BackgroundObject = null;
    [SerializeField]
    private GameObject Target = null;
    private float leftBounds, rightBounds, botBounds, topBounds;
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
        //leftBounds = (float)(xExtent - spriteBounds.sprite.bounds.size.x / 2.0f);
        //ightBounds = (float)(spriteBounds.sprite.bounds.size.x / 2.0f - xExtent);
        //   botBounds = (float)(yExtent - spriteBounds.sprite.bounds.size.y / 2.0f);
        //   topBounds = (float)(spriteBounds.sprite.bounds.size.y / 2.0f - yExtent);
    }
    void Update()
    {
        float interpolation = speed * Time.deltaTime;
        camPos = new Vector3(targetTransform.position.x, transform.position.y, transform.position.z);
        camPos.x = Mathf.Lerp(this.transform.position.x, Target.transform.position.x, interpolation);
     //   camPos.y = Mathf.Lerp(this.transform.position.y, Target.transform.position.y, interpolation);
       // camPos.x = Mathf.Clamp(camPos.x, leftBounds, rightBounds);
           // set up to keep y in bounds, in event we decide to make y change with character as well
     //   camPos.y = Mathf.Clamp(transform.position.y, botBounds, topBounds);
        camPos.z = transform.position.z;
        this.transform.position = camPos;
    }
}
