using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

//script used to set spawn point when entering a new scene
public class SetSpawnPoint : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPoint;

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = spawnPoint;
        player.GetComponent<RoverController>().setSpawnDirection();
    }
}
