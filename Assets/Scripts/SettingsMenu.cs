using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsMenu;

    //creates an instance of the settings menu
    private static SettingsMenu settingsInstance = null;
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

    // Start is called before the first frame update
    void Start()
    {
        settingsMenu.SetActive(false);
    }
}
