using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MicaScript : GenericElement
{

    // Start is called before the first frame update
    void Start()
    {
        value = 3;
        GenerateRandomVariance();
    }

    // Update is called once per frame
    void Update()
    {

    }
}