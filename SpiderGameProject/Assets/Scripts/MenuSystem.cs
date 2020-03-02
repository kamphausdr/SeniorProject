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
 

    // Update is called once per frame
    void Update()
    {
        
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
        transferLevel(level);
        SceneManager.LoadScene(level);
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
