using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Save
{
    public string CurrentLevelOn;
    public List<Level> Levels;
    public int Score = 0;
    //public List<bool> levelsUnlocked;
    public Save(string currentLevelOn, List<Level> levels, int score)
    {
        CurrentLevelOn = currentLevelOn;
        Levels = levels;
        Score = score;
     //   this.levelsUnlocked = levelsUnlocked;
    }

    public Save()
    {
        CurrentLevelOn = null;
        Levels = new List<Level>();
        Score = 0;
     //   levelsUnlocked = new List<bool>();

    }
    
    /// <summary>
    /// Takes the name of a level, searches through the list of levels in the save file and returns first match if it exists.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Level FindLevel (string name)
    {
         foreach (Level level in Levels)
        {
            if (level.levelName == name)
                return level;
        }
        return null;
    }
    public Level FindLevel(Level level)
    {
        foreach (Level levelI in Levels)
        {
            if (levelI.Equals(level))
                return level;
     
        }
        return null;
    }

    /// <summary>
    /// Searches the list of levels and checks if given level is a prequesit for any other levels. Returns dependent level if it exists, null if none are found.
    /// </summary>
    /// <param name="reqLevel"></param>
    /// <returns></returns>
    public Level FindDependentLevel(Level reqLevel)
    {
     
        foreach (Level levelI in Levels)
        {
            if (levelI.dependentLevel.levelName == reqLevel.levelName)
                return reqLevel;
        }
              return null;
    }

}

/// <summary>
/// Represents the save information for a given level that has been completed. Used to track all information
/// </summary>
    [System.Serializable]
    public class Level
{
    /// <summary>
    /// Name of the Level
    /// </summary>
    public string levelName ="";
    /// <summary>
    /// Wether the conditions have been met to unlock the level to be playable
    /// </summary>
    public bool unlocked = false;
    /// <summary>
    /// Score earned for the level
    /// </summary>
    public int score = 0;
    /// <summary>
    /// Number of stars player earned based off completion and score.
    /// </summary>
    public int starsEarned = 0;
    /// <summary>
    /// Whether the level has been completed or not
    /// </summary>
    public bool levelCompleted = false;

    public Level dependentLevel = null;

    /// <summary>
    /// Created a new blank Level, assumed to be incomplete
    /// </summary>
    public Level()
    {
        score = 0;
        starsEarned = 0;
        levelCompleted = false;
        unlocked = false;
    }
    /// <summary>
    /// Creates an empty level with name alone set.
    /// </summary>
    /// <param name="levelName"></param>
    public Level(string levelName)
    {
        this.levelName = levelName ?? throw new ArgumentNullException(nameof(levelName));
    }

   

    public Level(string levelName, bool unlocked, int score, int starsEarned, bool levelCompleted)
    {
        this.levelName = levelName ?? throw new ArgumentNullException(nameof(levelName));
        this.unlocked = unlocked;
        this.score = score;
        this.starsEarned = starsEarned;
        this.levelCompleted = levelCompleted;
    }
}