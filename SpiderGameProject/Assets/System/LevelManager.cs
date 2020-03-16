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
    public GameObject player;
    public int pointsPerQuestion = 100;
    public int wrongQuestionPentalty = 25;
    private int numberOfQuestions;
    private Level levelData;
    private GameObject gameManagerObject;
    private GameManager gameManager;

    Scene scene;

    private void Awake()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();

        // get the level data from the save file( either new one or existing)
        levelData = gameManager.getLevel(levelName);
    }
    void Start()
    {
        scene = SceneManager.GetActiveScene();

        Debug.Log("Active Scene name is: " + scene.name + "\nActive Scene index: " + scene.buildIndex);

        //load();
        player = GameObject.Find("Player");
        // sets the number of questions in the level to how ever many question givers are placed...
        numberOfQuestions = FindObjectsOfType<QuestionGiver>().Length;

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
        int adjustedPointPenalty = Mathf.Min(questionsMissed,3) * wrongQuestionPentalty;
        scoreToAdd = pointsPerQuestion - adjustedPointPenalty;
        playerScore += scoreToAdd;

    }
    public void endLevel()
    {
    
         levelComplete = true;

        Scene scene = SceneManager.GetSceneByName("Overworld");
        float correctRatio = playerScore / numberOfQuestions * pointsPerQuestion;

        if (correctRatio == 0.9f)
            numberofStars = 3;

        else if (correctRatio >= 0.6f)
            numberofStars = 2;
        else
            numberofStars = 1;
        levelComplete = true;

        levelData.score = playerScore;
        levelData.starsEarned = numberofStars;
        levelData.levelCompleted = true;

        gameManager.unlockLevelDependents(levelData);

        gameManager.saveLevel(levelData);

        SceneManager.MoveGameObjectToScene(gameManagerObject, scene);

        SceneManager.LoadScene("Overworld");
   
    }

    public Level getLevel()
    {
        Level thisLevel = new Level(levelName,true, playerScore, numberofStars, true);
        return thisLevel;
    }

   

}
