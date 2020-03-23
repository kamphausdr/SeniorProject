using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    private int playerScore;
    private int numberofStars = 0;
    private bool levelComplete = false;
    public string levelName;

    public int pointsPerQuestion = 100;
    public int wrongQuestionPentalty = 25;
    private int numberOfQuestions = 1;
    private Level levelData;
    private GameObject gameManagerObject;
    private GameManager gameManager;


    private void Awake()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();

        // get the level data from the save file( either new one or existing)
        levelData = gameManager.getLevel(levelName);
        numberOfQuestions = FindObjectsOfType<QuestionGiver>().Length;
    }
    void Start()
    {

        // sets the number of questions in the level to how ever many question givers are placed...
      

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
        float correctRatio = playerScore / numberOfQuestions * pointsPerQuestion;

        if (correctRatio >= 0.9f)
            numberofStars = 3;

        else if (correctRatio >= 0.6f)
            numberofStars = 2;
        else
            numberofStars = 1;
        levelComplete = true;

        Debug.Log("Ending level: score: " + playerScore.ToString() + "Stars Earned: " + numberofStars);

        levelData.starsEarned = numberofStars;
        levelData.levelCompleted = levelComplete;
        levelData.score = playerScore;

        gameManager.unlockLevelDependents(levelData);
        gameManager.saveLevel(levelData);

        StartCoroutine(LoadMenu());
       

    }
    public IEnumerator LoadMenu()
    {
        string overworld =("Overworld");
        Scene newScene;
        Scene thisScene = SceneManager.GetActiveScene();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(overworld, LoadSceneMode.Additive);             
        // SceneManager.LoadScene(level);
        newScene = SceneManager.GetSceneByName(overworld);
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
