using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the settings for the player. Saves their settings
/// </summary>
public class PlayerSettings : MonoBehaviour
{
    [SerializeField]
    private Slider slider = null;
    [SerializeField]
    private AudioSource myAudio = new AudioSource();

    public void Awake()
    {
        // If there is no existing music key in the settings file, then create one.
        if (!PlayerPrefs.HasKey("music"))
        {
           // Create the entry
            PlayerPrefs.SetFloat("music", slider.value);
            myAudio.enabled = true;
            myAudio.volume = slider.value;
            PlayerPrefs.Save();
        }
        else
        {
            // otherwise load the settings and change the dials to match the save file
            float volume = PlayerPrefs.GetFloat("music");
            if (volume == 0.0f)
            // disable the music if the volume is 0
                myAudio.enabled = false;

            else
            {
                myAudio.enabled = true;
                myAudio.volume = volume;
                if(slider != null)
                slider.value = volume;

            }
        }
    }
    public void ToggleMusic()
    {
        // change the music from off to on, vice versa
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
