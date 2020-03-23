using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRate : MonoBehaviour
{
    public GameObject[] star;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void setStars(int stars)
    {
        for(int i = 0; i < stars; i++)
        {
            star[i].GetComponent<Animator>().SetBool("Filled", true);
        }
    }
    public void show()
    {
        for (int i = 0; i < 3; i++)
        {
            SpriteRenderer getStar = star[i].GetComponent<SpriteRenderer>();
            getStar.enabled = true;
        }
    }
    public void hide()
    {
        for (int i = 0; i < 3; i++)
        {
            SpriteRenderer getStar = star[i].GetComponent<SpriteRenderer>();
            getStar.enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
