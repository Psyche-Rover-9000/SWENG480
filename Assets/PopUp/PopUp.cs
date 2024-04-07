using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public static bool popUpElementActive;
    public static bool popUpUpgradeActive;
    public static bool popUpFactsActive;
    public GameObject popUp;
    public int pageNum = 1;

    public void clickOK() //closes the popup
    {
        if (popUp.name == "PopUpPanel") //if element pop up, not upgrade pop up
        {
            //close any current element info
            popUp.transform.Find("IronInfo").gameObject.SetActive(false);
            popUp.transform.Find("RockInfo").gameObject.SetActive(false);
            popUp.transform.Find("MicaInfo").gameObject.SetActive(false);
            popUp.transform.Find("NickelInfo").gameObject.SetActive(false);
            popUp.transform.Find("OlivineInfo").gameObject.SetActive(false);
            popUp.transform.Find("FeldsparInfo").gameObject.SetActive(false);
            popUp.transform.Find("PyroxeneInfo").gameObject.SetActive(false);
            popUp.transform.Find("QuartzInfo").gameObject.SetActive(false);

            //mark element pop up as closed
            popUpElementActive = false;

        }
        else if(popUp.name == "PopUpPanelFacts")
        {
            popUp.transform.Find("Fact1").gameObject.SetActive(false);
            popUp.transform.Find("Fact2").gameObject.SetActive(false);
            popUp.transform.Find("Fact3").gameObject.SetActive(false);
            popUp.transform.Find("Fact4").gameObject.SetActive(false);
            popUp.transform.Find("Fact5").gameObject.SetActive(false);

            //mark element pop up as closed
            popUpFactsActive = false;
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

    public void closeJournal()
    {
        //close whatever fact is open
        popUp.transform.Find("Fact1").gameObject.SetActive(false);
        popUp.transform.Find("Fact2").gameObject.SetActive(false);
        popUp.transform.Find("Fact3").gameObject.SetActive(false);
        popUp.transform.Find("Fact4").gameObject.SetActive(false);
        popUp.transform.Find("Fact5").gameObject.SetActive(false);

        //set text objects for new journal pickup
        popUp.transform.Find("NewJournalTipText").gameObject.SetActive(true);
        popUp.transform.Find("NewFactText").gameObject.SetActive(true);
        popUp.transform.Find("RereadFactText").gameObject.SetActive(false);

        //switch buttons
        popUp.transform.Find("OKButton").gameObject.SetActive(true);
        popUp.transform.Find("CloseJournalButton").gameObject.SetActive(false);
        popUp.transform.Find("NextButton").gameObject.SetActive(false);
        popUp.transform.Find("PrevButton").gameObject.SetActive(false);

        //set pageNum back to 1
        pageNum = 1;

        //mark journal pop up as closed
        popUpFactsActive = false;

        //close pop up
        popUp.SetActive(false);

    }

    public void clickPrev()
    {
        popUp.transform.Find("Fact" + pageNum.ToString()).gameObject.SetActive(false);
        pageNum--;
        popUp.transform.Find("Fact" + pageNum.ToString()).gameObject.SetActive(true);

        //show next button 
        popUp.transform.Find("NextButton").gameObject.SetActive(true);

        //if no prev page, hide prev button
        if (pageNum == 1)
        {
            popUp.transform.Find("PrevButton").gameObject.SetActive(false);
        }
    }

    public void clickNext()
    {
        //show correct fact
        popUp.transform.Find("Fact" + pageNum.ToString()).gameObject.SetActive(false);
        pageNum++;
        popUp.transform.Find("Fact" + pageNum.ToString()).gameObject.SetActive(true);

        //show prev button
        popUp.transform.Find("PrevButton").gameObject.SetActive(true);

        // check how many pages user has and hide next button if user doesn't have next fact
        GameObject rover = GameObject.FindWithTag("Player");
        int numCollectedPages = rover.GetComponent<RoverController>().numberOfPages;

        if (numCollectedPages - 1 == pageNum) // subtract 1 from numCollectedPages because rover controller keeps track of journals differently than pop up page numbers
        {
            popUp.transform.Find("NextButton").gameObject.SetActive(false); //hide next button if user hasn't found next
        }
    }
}

