using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Load Scene
    public void play()
    {
        SceneManager.LoadScene("CaveScene");                                                                   
    }
}
