﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
/// <summary>
/// This handles the Main Menu. It allows the player to navigate through the game.
/// </summary>
public class MenuSystem : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject SettingsUI;
    // Start is called before the first frame update
    private GameObject gameManager;
  //  public string levelDir = "Levels/";
    private bool guiShow = false;
    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }
    void OnGUI()
    {
        bool alert = false;
        int x, y, xGap;
        x = 400;
        y = 200;
        xGap = 200;
        if (guiShow)
        {
           
            GUI.Box(new Rect(x, y, 400, 25), "Are you sure you want to delete your file, all progress will be lost.");
            if (GUI.Button(new Rect(x+ 100 , y +50 , 100, 30), "No"))
            {
                guiShow = false;
            }
            if (GUI.Button(new Rect(x + xGap, y + 50,  100, 30), "Yes"))
            {
      
                deleteConfirm();
              
                alert = true;
    
                guiShow = false;
            }
        }
        if (alert)
        {
            GUI.Box(new Rect(x, y, 400, 25), "File Deleted");
            if (GUI.Button(new Rect(x + 130, y + 50, 100, 30), "Ok"))
            {
                alert = false;
            }
        }

    }
    public  void QuitButton()
    {
        Application.Quit();
    }
    public  void DeleteDataButton()
    {
        guiShow = true;
     //   DialogUI.SetActive(true);
    }


   public void deleteConfirm()
    {

            string filepath = Application.persistentDataPath + "/gamesave.save";
            if (File.Exists(filepath))
            {
                Debug.Log("Deleting file");
                File.Delete(filepath);
       
                guiShow = true;
               // OnGui();
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
                #endif
            }
            else
            {
                Debug.Log("File not Found");

         
                guiShow = true;
            }
    }
    
    public void StartButton()
    {
        SceneManager.LoadScene("Overworld");
    }
    public void SettingsButton()
    {
        SettingsUI.SetActive(true);
        MainMenuUI.SetActive(false);
    }
    public void LoadLevel(string level)
    {
        StartCoroutine(LoadLevelAsync(level));
    }
    public  IEnumerator LoadLevelAsync(string level)
    {
       // string levelPath = levelDir + level;
        Scene newScene; 
        Scene thisScene = SceneManager.GetActiveScene();
   
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
    
        newScene = SceneManager.GetSceneByName(level);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.MoveGameObjectToScene(gameManager, newScene);
        SceneManager.UnloadSceneAsync(thisScene);
    }
    public void Back()
    {
        SettingsUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }
    }
