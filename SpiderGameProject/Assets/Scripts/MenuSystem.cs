using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuSystem : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject SettingsUI;
    // Start is called before the first frame update
    private GameObject gameManager;

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    public  void QuitButton()
    {
        Application.Quit();
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
        Scene newScene; 
        Scene thisScene = SceneManager.GetActiveScene();
        //  transferLevel(level);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
       // SceneManager.LoadScene(level);
        newScene = SceneManager.GetSceneByName(level);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.MoveGameObjectToScene(gameManager, newScene);
        SceneManager.UnloadSceneAsync(thisScene);
    }


    private void transferLevel(string levelName)
    {
     //   Scene sceneToLoad;
       // sceneToLoad = SceneManager.GetSceneByName(levelName);
       // SceneManager.MoveGameObjectToScene(this.GetComponentInParent<GameObject>(), sceneToLoad);
    }
    public void Back()
    {
        SettingsUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }
    }
