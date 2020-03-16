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
    public void LoadDemo()
    {

        transferLevel("Test Level");
        SceneManager.LoadScene("Test Level");
        
    }
    public void LoadLevel(string level)
    {
        Scene scene = SceneManager.GetSceneByName(level);
      //  transferLevel(level);

     
        SceneManager.LoadScene(level);
        SceneManager.MoveGameObjectToScene(gameManager, scene);
        Debug.Log("GameManager should be moved?");
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
