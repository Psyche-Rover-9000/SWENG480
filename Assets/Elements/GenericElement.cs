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
 * this is the class that all element classes connect to. 
 * 
 * Created 10/30/2023
 * 
 * changelog: 11/5/2023 - added soundEffect to play a audio clip when the element is colected.
 */

    protected int value = 0;                          //the points this element has. hard code this in the Start() of actual element.
    public Sprite[] thisElementVariance;           //the sprites that this element can have.
    protected string element;
    List<Vector3> elementCoordinates;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        GameObject rover = GameObject.FindWithTag("Player");
        elementCoordinates = rover.GetComponent<RoverController>().elementCoordinates; // get list of coordinates of already collected elements from rover 

        foreach (Vector3 coordinates in elementCoordinates) //for each set of coordinates rom collected elements
        {
            if (gameObject.transform.position == coordinates) //if element from that position has already been collected, delete it
            {
                Destroy(gameObject);
            }
        }
    }

    //this method set the sprite to a ramdom alternate sprite for the element.
    protected void GenerateRandomVariance()
    {
        SpriteRenderer thisElement = gameObject.GetComponent<SpriteRenderer>();

        int ramdomNumber = Random.Range(0, thisElementVariance.Length);
        thisElement.sprite = thisElementVariance[ramdomNumber];


    }

    //the method to run when the Element is colected. it will return the value of the element.
    public int getElement()
    {
        gameObject.SetActive(false);

        return value;
    }

    public Vector3 getCoordinates()
    {
        return gameObject.transform.position; //return this element's coordinates
    }
}
