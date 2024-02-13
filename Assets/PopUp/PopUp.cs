using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public static bool popUpElementActive;
    public static bool popUpUpgradeActive;
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

            //mark element pop up ad closed
            popUpElementActive = false;
        }
        else if (popUp.name == "UpgradePopUp") // if upgrade pop up, not element pop up
        {
            //close any current upgrade info
            popUp.transform.Find("BoostInfo").gameObject.SetActive(false);
            popUp.transform.Find("TransmitterInfo").gameObject.SetActive(false);
            popUp.transform.Find("FlashlightInfo").gameObject.SetActive(false);
            popUp.transform.Find("GrabberInfo").gameObject.SetActive(false);

            //mark upgrade pop up as closed
            popUpUpgradeActive = false;
        }

        //close pop up
        popUp.SetActive(false);
    }
}
