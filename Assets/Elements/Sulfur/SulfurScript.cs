using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SulfurScript : GenericElement
{

    // Start is called before the first frame update
    void Start()
    {
        value = 2;
        GenerateRandomVariance();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
