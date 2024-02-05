using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    public void play() // load game
    {
        SceneManager.LoadScene("Disclaimer"); //load disclaimer scene
    }
}
