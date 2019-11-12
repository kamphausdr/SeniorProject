using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private AudioSource myAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Awake()
    {
        if (!PlayerPrefs.HasKey("music"))
        {
           
            PlayerPrefs.SetFloat("music", slider.value);
            myAudio.enabled = true;
            myAudio.volume = slider.value;
            PlayerPrefs.Save();
        }
        else
        {
            float volume = PlayerPrefs.GetFloat("music");
            if (volume == 0.0f)
                myAudio.enabled = false;
            else
            {
                myAudio.enabled = true;
                myAudio.volume = volume;
                slider.value = volume;

            }
        }
    }
    public void ToggleMusic()
    {
        PlayerPrefs.SetFloat("music", slider.value);
        myAudio.volume = slider.value;
        if (slider.value > 0)
        {
           
            myAudio.enabled = true;
        
        }
        else
        {
             myAudio.enabled = false;
        }
        PlayerPrefs.Save();
    }


}
