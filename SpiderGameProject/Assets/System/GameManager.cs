using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{ 
    public int LevelMaxScore = 1000;
    private int playerScore;
    private int questionsWrong;
    private int numberofStars = 0;
    private bool levelComplete;
    public string levelName;
    public GameObject player;
    // Start is called before the first frame update
    public void questionWrong(int numWrong)
    {
        questionsWrong += numWrong;
    }
    public int getScore()
    {
        return playerScore;
    }
    public void AddPoints(int points)
    {
        playerScore += points;
    }
    private void updateScore()
    {
        playerScore = 1000 - questionsWrong * 100;
    }
    public void endLevel()
    {
        updateScore();
        if (playerScore == LevelMaxScore)
            numberofStars = 3;

        else if (playerScore >= LevelMaxScore / 2)
            numberofStars = 2;
        else
            numberofStars = 1;
        levelComplete = true;
        SceneManager.LoadScene("Overworld");
        // save();
    }
    private Save CreateSaveObject()
    {
        Save save = new Save();
      

        Level level = new Level();
        level.score = getScore();
        level.starsEarned = numberofStars;
        level.levelName = levelName;
        if (!levelComplete)
        {
            foreach (QuestionGiver questionGiver in FindSceneObjectsOfType(typeof(QuestionGiver)))
            {
                bool newGiverBool;
                 newGiverBool = questionGiver.enabled;
                level.questionGiverEnabled.Add(newGiverBool);
                // level.questionGivers.Add(questionGiver);

            }
        }
        level.playerPosition = player.transform.position;
        save.Levels.Add(level);
        return save;
    }
    public void save()
    {
        Save save = CreateSaveObject();
        Debug.Log("Attempting to save");
        BinaryFormatter bin = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "gamesave.save");
        bin.Serialize(file, save);
        file.Close();


       
    }
    public void load()
    {
        Debug.Log("Attempting to load");
        if (File.Exists(Application.persistentDataPath+ "/gamesave.save"))
        {
            Debug.Log("Supposedly loaded?");
            BinaryFormatter bin = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bin.Deserialize(file);
            file.Close();
            Level myLevel = save.FindLevel(levelName);
            int i = 0;
           foreach (QuestionGiver questionGiver in FindSceneObjectsOfType(typeof(QuestionGiver)))
            {
                bool newGiverBool;
                newGiverBool = questionGiver.enabled;
                questionGiver.enabled = myLevel.questionGiverEnabled[i];
                // level.questionGivers.Add(questionGiver);
                i++;
            }
            playerScore = myLevel.score;
            player.transform.position = myLevel.playerPosition;

        }
    }
    void Start()
    {
       
        load();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
