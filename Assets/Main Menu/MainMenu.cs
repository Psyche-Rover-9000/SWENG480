using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    public void play() // load game
    {
        SceneManager.LoadScene("MainWorld"); //load main world scene
    }

    public void credits() // load credits scene
    {
        SceneManager.LoadScene("Credits"); //load Credits
    }
}
