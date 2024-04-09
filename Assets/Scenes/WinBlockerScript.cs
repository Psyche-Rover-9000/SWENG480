using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBlockerScript : MonoBehaviour
{
    private GameObject roverPlayer;
    private RoverController roverScript;
    private int roverScore;

    // Start is called before the first frame update
    void Start()
    {
        GameObject roverPlayer = GameObject.FindWithTag("Player");
        roverScript = roverPlayer.GetComponent<RoverController>();
    }

    // Update is called once per frame
    void Update()
    {
        //destroy blocker if rover has collected all elements
        if (!roverScript.ironIsNew && !roverScript.rockIsNew && !roverScript.micaIsNew && !roverScript.nickelIsNew && !roverScript.olivineIsNew && !roverScript.feldsparIsNew && !roverScript.pyroxeneIsNew && !roverScript.quartzIsNew)
        {
            Destroy(gameObject);
        }
    }
}
