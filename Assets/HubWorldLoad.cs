using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorldLoad : MonoBehaviour
{
    public GameObject NASABase;

    void Start() //every time hub world is loaded:
    {
        //set rover spawn to saved coordinates in rover controller script
        GameObject rover = GameObject.FindWithTag("Player");
        rover.GetComponent<RoverController>().moveSpawn();

        //check if element is collected. dont need to check for rock, it is always in nasa base
        RoverController roverScript = rover.GetComponent<RoverController>();
        if (!roverScript.ironIsNew)
        {
            NASABase.transform.Find("IronNASA").gameObject.SetActive(true); //if element has been found, element appears in nasa base of hub world
        }
        if (!roverScript.micaIsNew)
        {
            NASABase.transform.Find("MicaNASA").gameObject.SetActive(true);
        }
        if (!roverScript.nickelIsNew)
        {
            NASABase.transform.Find("NickelNASA").gameObject.SetActive(true);
        }
        if (!roverScript.olivineIsNew)
        {
            NASABase.transform.Find("OlivineNASA").gameObject.SetActive(true);
        }
        if (!roverScript.feldsparIsNew)
        {
            NASABase.transform.Find("FeldsparNASA").gameObject.SetActive(true);
        }
        if (!roverScript.pyroxeneIsNew)
        {
            NASABase.transform.Find("PyroxeneNASA").gameObject.SetActive(true);
        }
        if (!roverScript.quartzIsNew)
        {
            NASABase.transform.Find("QuartzNASA").gameObject.SetActive(true);
        }
    }

}
