using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// This object represents a level within the overworld. This supplies some information about the level that it obtains from the game manager
/// </summary>
public class LevelItem : MonoBehaviour
{
    private Level level;
    public string levelName;
    private bool available;
    private GameManager gameManager;
    private Button button;
    public GameObject Collectable;
    public Text scoreLabel;
    public StarRate starGroup;
    const string SCORELABEL = "Score: ";

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        level = gameManager.getLevel(levelName);
        button = GetComponent<Button>();
        available = level.unlocked;
        GameObject uiGroup = GameObject.Find(levelName + " Group");
        if(available)
        {
            starGroup.show();
            uiGroup.SetActive(true);

            button.enabled = true;
            starGroup.setStars(level.starsEarned);
            Collectable.GetComponent<Animator>().SetBool("Unlocked", level.levelCompleted);
            scoreLabel.text = SCORELABEL + level.score;
        }
        else
        {
            uiGroup.SetActive(false);
            button.enabled = false;
            starGroup.hide();
        }
    }
}
