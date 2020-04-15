using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
/// <summary>
/// Manages all the data in a given level, the glue that holds the level together and interacts with the game manager
/// </summary>
public class LevelManager : MonoBehaviour
{
    private int playerScore = 0;
    private int numberofStars = 0;
    private bool levelComplete = false;
    public string levelName;

    public int pointsPerQuestion = 100;
    public int wrongQuestionPentalty = 25;
    public int numberOfQuestions = 1;
    private Level levelData;
    private GameObject gameManagerObject;
    private GameManager gameManager;


    private void Awake()
    {
        gameManagerObject = GameObject.Find("GameManager");
        try
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
        // Literally for the purposes of debugging, this exception is only thrown when loading a level because gameobject isn't loaded
        // from the other overworld
        catch (System.NullReferenceException)
        {
            //Create a temp gamemanager for purposes of debugging to keep null exception errors.
            Debug.Log("Game Manager not functioning in local debug mode, level will not save.");
            gameManagerObject = new GameObject("TempManager",typeof(GameManager)) ;
           
        }


        // get the level data from the save file( either new one or existing)
        try
        {
            levelData = gameManager.getLevel(levelName);
        }
        catch (System.NullReferenceException)
        {

            levelData = new Level();
        }
    }

    public int getScore()
    {
        return playerScore;
    }
    public void AddPoints()
    {
        playerScore += pointsPerQuestion;
    }
    public void AddPoints(int questionsMissed)
    {
        int scoreToAdd = 0;

        // penalty wont go more than 3 questions worth, thus min of the questions or 3 whichever is smaller.
        int adjustedPointPenalty = Mathf.Min(questionsMissed, 3) * wrongQuestionPentalty;
        scoreToAdd = pointsPerQuestion - adjustedPointPenalty;
        playerScore += scoreToAdd;

    }
    public void endLevel()
    {
        levelComplete = true;
        Scene thisScene = SceneManager.GetActiveScene();
        float correctRatio = (float) playerScore / (float) (numberOfQuestions * pointsPerQuestion);

        if (correctRatio >= 0.9f)
            numberofStars = 3;

        else if (correctRatio >= 0.6f)
            numberofStars = 2;
        else
            numberofStars = 1;
        levelComplete = true;
     
        Debug.Log("Ending level: score: " + playerScore.ToString() + "Stars Earned: " + numberofStars + "Ratio" + correctRatio);

        levelData.starsEarned = numberofStars;
        levelData.levelCompleted = levelComplete;
        levelData.score = playerScore;

        gameManager.unlockLevelDependents(levelData);
        gameManager.saveLevel(levelData);

        // Special unity procedure for loading the menu asynchronusly - allows multithreading
        StartCoroutine(LoadMenu());
       

    }
    //Loads the menu, IEnumerator is required for the aysnchronus loading with unity
    public IEnumerator LoadMenu()
    {
        string overworld =("Overworld");
        Scene newScene;
        Scene thisScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(overworld, LoadSceneMode.Additive);             
        // SceneManager.LoadScene(level);
        newScene = SceneManager.GetSceneByName(overworld);
        // Required code for the async loading
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.MoveGameObjectToScene(gameManagerObject, newScene);
        SceneManager.UnloadSceneAsync(thisScene);
    }
    public Level getLevel()
    {
        Level thisLevel = new Level(levelName,true, playerScore, numberofStars, true);
        return thisLevel;
    }
}
