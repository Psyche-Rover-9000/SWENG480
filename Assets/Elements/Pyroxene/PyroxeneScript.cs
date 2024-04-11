using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PyroxeneScript : GenericElement
{

    // Start is called before the first frame update
    void Start()
    {
        value = 750;
        GenerateRandomVariance();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

