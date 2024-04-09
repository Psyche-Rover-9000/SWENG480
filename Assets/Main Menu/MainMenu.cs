using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    public void play() // load game
    {
        SceneManager.LoadScene("HubWorld"); //load main world scene
    }

    public void credits() // load credits scene
    {
        SceneManager.LoadScene("Credits"); //load Credits
    }

    public void settings() //open settings
    {
        gameObject.SetActive(false); //close main menu
    }
}
