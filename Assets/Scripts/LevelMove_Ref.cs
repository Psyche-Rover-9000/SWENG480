using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class LevelMove_Ref : MonoBehaviour
{
    public int sceneBuildIndex;

    // Level move zoned enter, if collider is a player
    // Move game to another scene
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");

        // Could use other.GetComponent<Player>() to see if the game object has a Player component
        // Tags work too. Maybe some players have different script components?
        if (other.tag == "Player")
        {
            //if in hub world or cave 4 (scenes with multiple cave entrances)
            if (SceneManager.GetActiveScene().name == "HubWorld" || SceneManager.GetActiveScene().name == "Cave4")
            {
                other.GetComponentInParent<RoverController>().setSpawn(gameObject.transform.position);  // set spawn in rover controller for when returning to hub world/cave 4
            }

            // Player entered, so move level
            print("Switching Scene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}
