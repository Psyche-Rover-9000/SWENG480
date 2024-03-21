using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBlockerScript : MonoBehaviour
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
        roverScore = roverScript.roverLevel;
        if (roverScore >= 3)
        {
            Destroy(gameObject);
        }
    }
}
