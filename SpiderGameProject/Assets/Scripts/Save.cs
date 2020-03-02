using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Save
{

    public string CurrentLevelOn;
    public List<Level> Levels;
    public int Score = 0;
    public List<bool> levelsUnlocked;
    public Level FindLevel (string name)
    {
         foreach (Level level in Levels)
        {
            if (level.levelName == name)
                return level;
        }
        return null;
    }
    // Start is called before the first frame update
}

    [System.Serializable]
    public class Level
{
    public string levelName ="";
    public bool unlocked = false;
    public int score = 0;
    public int starsEarned = 0;
    public bool levelCompleted = false;

    public Vector2 playerPosition;
    public List<bool> questionGiverEnabled = new List<bool>();
    public Level()
    {
        score = 0;
        starsEarned = 0;
    }
    
   // public List<int> remainingQuestionGiversTypes = new List<int>();
}