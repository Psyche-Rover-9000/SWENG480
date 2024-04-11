using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NickelScript : GenericElement
{

    // Start is called before the first frame update
    void Start()
    {
        value = 100;
        GenerateRandomVariance();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
