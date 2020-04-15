using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;
using System;
/// <summary>
/// Gamemanger handles all the game data for the game. It travels between levels and acts as the glue that holds the game together.
/// </summary>
public class GameManager : MonoBehaviour
{
    private Save currentData;
    private const string fileName = "/gamesave.save";
   // private LevelManager currentLevel;
    // Start is called before the first frame update
    void Awake()
    {
        load();
    }
    public int getScore()
    {
        return currentData.Score;
        
    }
    public Level getLevel(string name)
    {
        return currentData.FindLevel(name);
    }
    public Level getLevel(Level level)
    {
        return currentData.FindLevel(level);
    }
    /// <summary>
    /// Gets the level data from other source and updates data store for that level.
    /// </summary>
    /// <param name="level"></param>
    private void takeLevelData(Level level)
    {
        // find the old level based solely off name
        Level oldLevel = getLevel(level.levelName);
        // get the index of old level so we can change the value of the level.
        int index = currentData.Levels.IndexOf(oldLevel);
        currentData.Levels[index] = level;
    }
    public void unlockLevelDependents(Level reqLevel)
    {
        // get the level it depends on
        Level dependentLevel = currentData.FindDependentLevel(reqLevel);
        if(dependentLevel != null)
        {
            currentData.FindLevel(dependentLevel.levelName).unlocked = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private Save CreateSaveObject()
    {
        Save save = new Save();
        Level kitchen = new Level("Kitchen Level");
        Level gardenLevel = new Level("Garden Level");
        Level cyberLevel = new Level("Cyber Level");
        kitchen.dependentLevel = gardenLevel;
        cyberLevel.dependentLevel = kitchen;
        gardenLevel.unlocked = true;
        save.Levels.Add(kitchen);
        save.Levels.Add(gardenLevel);
        save.Levels.Add(cyberLevel);
        save.CurrentLevelOn = gardenLevel.levelName;
        // make list of all the levels 

        return save;
    }
    //Write the game data to the disk for any given data
    public void save(Save dataFile)
    {
        Debug.Log("Attempting to save");
        BinaryFormatter bin = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + fileName);
        bin.Serialize(file, dataFile);
        file.Close();
    }
    public void saveLevel(Level level)
    {
        takeLevelData(level);
        save();
    }
    public string test()
    {
        return this.name;
    }
    // simple save that uses the data already in the object
    public void save()
    {
        save(currentData);   
    }
    // Loads the game data from the disk
    public void load()
    {
        Save saveData;
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            BinaryFormatter bin = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            saveData = (Save)bin.Deserialize(file);
            file.Close();
        }
        else
        {
            saveData = CreateSaveObject();
        }
        currentData = saveData;   
    }
}
