using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelItem : MonoBehaviour
{
    private Level level;
    public string levelName;
    private bool available;
    private GameManager gameManager;
    private Button button;
    public Text scoreLabel;
    public StarRate starGroup;
    const string SCORELABEL = "Score: ";
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
       // gameManager.load();
        level = gameManager.getLevel(levelName);
        button = GetComponent<Button>();
        available = level.unlocked;
        if(available)
        {
            Debug.Log("Unlocked, setting stars!");
            starGroup.show();
            button.enabled = true;
           starGroup.setStars(2);
            scoreLabel.text = SCORELABEL + level.score;
        }
        else
        {
            Debug.Log("Locked Disabling");
            button.enabled = false;
            starGroup.hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
