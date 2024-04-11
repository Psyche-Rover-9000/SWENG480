using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuartzScript : GenericElement
{

    // Start is called before the first frame update
    void Start()
    {
        value = 500;
        GenerateRandomVariance();
    }

    // Update is called once per frame
    void Update()
    {

    }
}