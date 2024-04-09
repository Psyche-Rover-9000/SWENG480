using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuPause : MonoBehaviour // script for the settings menu that is connected to pause menu objects
{
    public GameObject pauseMenu;

    //creates an instance of the pause menu's settings menu. this settings menu is kept even if user goes back to main menu so that pause objects don't disconnect from settings
    private static SettingsMenuPause settingsInstance = null;
    private void Awake()
    {
        if (settingsInstance == null)
        {
            settingsInstance = this; //set instance
            DontDestroyOnLoad(this.gameObject); //used to keep the same object in all scenes
            return;
        }
        Destroy(this.gameObject); //destroy duplicate settings menu objects when returning to MainWorld
    }

    public void GoToSettings()
    {
        pauseMenu.SetActive(false);
        gameObject.SetActive(true);
    }
}
