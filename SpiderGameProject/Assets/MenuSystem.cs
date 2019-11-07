﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public  void QuitButton()
    {
        Application.Quit();
    }
    public void StartButton()
    {
        SceneManager.LoadScene("Overworld");
    }
    public void LoadDemo()
    {
        SceneManager.LoadScene("Test Level");
    }
    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
    }
