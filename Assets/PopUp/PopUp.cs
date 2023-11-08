using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public static bool popUpActive;
    public GameObject popUp;

    public void clickOK() //closes the popup
    {
        popUp.SetActive(false);
        popUpActive = false;
    }
}
