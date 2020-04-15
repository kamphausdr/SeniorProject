using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A simple class that triggers end of level conditions when player collides with the object
/// </summary>
public class collectable : MonoBehaviour
{
    public LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            levelManager.endLevel();


        }
    }
   }

