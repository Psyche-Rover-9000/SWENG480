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
 * changelog: 11/5/2023 - added soundEffect to play a audio clip when the element is colected.
 */

    protected int value = 0;                          //the points this element has. hard code this in the Start() of actual element.
    public Sprite[] thisElementVariance;           //the sprites that this element can have.
   // public AudioSource soundEffect;                  //the sound effect of the element colecting.

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
        //soundEffect.Play();   //sound effect is now played in RoverController.cs
        gameObject.SetActive(false);
        return value;
    }


}
