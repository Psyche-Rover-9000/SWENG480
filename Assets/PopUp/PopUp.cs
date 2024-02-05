using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public static bool popUpActive;
    public GameObject popUp;

    public void clickOK() //closes the popup
    {
        if (popUp.name == "PopUpPanel") //if element pop up, not upgrade pop up
        {
            //close any current element info
            popUp.transform.Find("IronInfo").gameObject.SetActive(false);
            popUp.transform.Find("SapphireInfo").gameObject.SetActive(false);
            popUp.transform.Find("AluminumInfo").gameObject.SetActive(false);
            popUp.transform.Find("TungstenInfo").gameObject.SetActive(false);
            popUp.transform.Find("SulfurInfo").gameObject.SetActive(false);
        }

        //close pop up
        popUp.SetActive(false);
        popUpActive = false;
    }
}
