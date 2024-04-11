using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeldsparScript : GenericElement
{
    // Start is called before the first frame update
    void Start()
    {
        value = 150;
        GenerateRandomVariance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
