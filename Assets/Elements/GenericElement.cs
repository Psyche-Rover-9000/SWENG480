using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;


public abstract class GenericElement : MonoBehaviour
{
    /*
 * Author: Jacob Meyer
 * 
 * this is the class that all element classes conect to. 
 * 
 * Created 10/30/2023
 * 
 * changelog: none yet.
 */

    protected int value = 0;                          //the points this element has. hard code this in the Start() of actual element.
    public Sprite[] thisElementVariance;           //the sprites that this element can have.


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this method set the sprite to a ramdom alternate sprite for the element.
    protected void GenerateRandomVariance()
    {
        SpriteRenderer thisElement = gameObject.GetComponent<SpriteRenderer>();
         
        int ramdomNumber = Random.Range(0 , thisElementVariance.Length);
        thisElement.sprite = thisElementVariance[ramdomNumber];


    }

    //the method to run when the Element is colected. it will return the value of the element.
    public int getElement()
    {
        gameObject.SetActive(false);
        return value;
    }


}