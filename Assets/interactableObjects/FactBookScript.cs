using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class FactBookScript : MonoBehaviour
{
    List<Vector3> journalCoordinates; // coordinates of collected journals

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
        journalCoordinates = rover.GetComponent<RoverController>().journalCoordinates; // get list of coordinates of already collected journals from rover 

        foreach (Vector3 coordinates in journalCoordinates) //for each set of coordinates from collected journals
        {
            if (gameObject.transform.position == coordinates) //if element from that position has already been collected, delete it
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
        }
    }

    public void getFact()
    {
        gameObject.SetActive(false);
       
    }

    public Vector3 getCoordinates()
    {
        return gameObject.transform.position; //return this element's coordinates
    }
}
