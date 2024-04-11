using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IronScript : GenericElement
{
    
    // Start is called before the first frame update
    void Start()
    {
        value = 75;
        GenerateRandomVariance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
