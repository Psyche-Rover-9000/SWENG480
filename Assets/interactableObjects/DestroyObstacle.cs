using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    private GameObject roverPlayer;
    private RoverController roverScript;
    private int roverScore;
    List<Vector3> obstacleCoordinates;

    // Start is called before the first frame update
    void Start()
    {
        GameObject roverPlayer = GameObject.FindWithTag("Player");
        roverScript = roverPlayer.GetComponent<RoverController>();
        
    }

    private void Awake()
    {
        GameObject rover = GameObject.FindWithTag("Player");
        obstacleCoordinates = rover.GetComponent<RoverController>().obstacleCoordinates; // get list of coordinates of already destroyed obstacles from rover 

        foreach (Vector3 coordinates in obstacleCoordinates) //for each set of coordinates from obstacles list
        {
            if (gameObject.transform.position == coordinates) //if obstacle from that position has already been destoryed, delete it
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //before destroiying object:
        //store coordinates of the collected obstacle to list in (ddol) rover controller
        Vector3 coordinates = gameObject.transform.position;
        roverScript.obstacleCoordinates.Add(coordinates);

        // destroy object
        roverScore = roverScript.roverLevel;
        if(roverScore >= 2 && Input.GetKey(KeyCode.Space))
        {
            Destroy(gameObject);
        }
        
    }
}
