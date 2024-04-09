using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour //script for the settings menu connected to main menu objects
{
    public GameObject mainMenu;

    public void Back()
    {
        mainMenu.SetActive(true); //reopen main menu
    }
}
