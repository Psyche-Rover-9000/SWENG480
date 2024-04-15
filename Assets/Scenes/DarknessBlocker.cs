using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessBlocker : MonoBehaviour
{
    private GameObject roverPlayer;
    private RoverController roverScript;
    private int roverScore;

    // Start is called before the first frame update
    void Start()
    {
        GameObject roverPlayer = GameObject.FindWithTag("Player");
        roverScript = roverPlayer.GetComponent<RoverController>();
    }

    // Update is called once per frame
    void Update()
    {
        roverScore = roverScript.roverLevel;
        if (roverScore >= 4)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }
}
}
