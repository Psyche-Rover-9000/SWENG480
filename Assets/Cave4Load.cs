using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave4Load : MonoBehaviour
{
    void Start()
    {
        //set rover spawn to saved coordinates in rover controller script
        GameObject rover = GameObject.FindWithTag("Player");
        rover.GetComponent<RoverController>().moveSpawn();
    }
}
