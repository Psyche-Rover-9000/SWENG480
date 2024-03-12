using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    public void clickOK()
    {
        SceneManager.LoadScene("PsycheRover9000"); //load main menu
    }

}
