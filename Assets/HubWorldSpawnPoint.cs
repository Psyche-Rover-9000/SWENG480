using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorldSpawnPoint : MonoBehaviour
{
    private void Start() //every time hub world is loaded, set rover spawn to saved coordinates in rover controller script
    {
        GameObject rover = GameObject.FindWithTag("Player");
        rover.GetComponent<RoverController>().moveSpawn();
    }
}
