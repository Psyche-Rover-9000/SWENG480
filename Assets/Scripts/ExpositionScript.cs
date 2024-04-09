using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExpositionScript : MonoBehaviour
{
    public void clickOK()
    {
        SceneManager.LoadScene("HubWorld"); //load hub world
    }
}
