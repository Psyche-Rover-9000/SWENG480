using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObject : MonoBehaviour
{

    public GameObject[] myObjects;

    void Start()
    {
        {
            int randomIndex = Random.Range(0, myObjects.Length);
            
            Instantiate(myObjects[randomIndex], transform.position, Quaternion.identity);
        }
    }
}