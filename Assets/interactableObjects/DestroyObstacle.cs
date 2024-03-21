using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    public GameObject roverPlayer;
    public RoverController roverScript;
    public int roverScore;
    // Start is called before the first frame update
    void Start()
    {
        GameObject roverPlayer = GameObject.FindWithTag("Player");
        roverScript = roverPlayer.GetComponent<RoverController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        roverScore = roverScript.roverLevel;
        if(roverScore >= 2 && Input.GetKey(KeyCode.Space))
        {
            Destroy(gameObject);
        }
        
    }
}
