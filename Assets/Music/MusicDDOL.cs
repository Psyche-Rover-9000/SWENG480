using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDDOL : MonoBehaviour
{
    //creates an instance of the game sound
    private static MusicDDOL musicInstance = null;
    private void Awake()
    {
        //ignore MainWorld duplication when music object is MenuMusic
        if (gameObject.name == "MenuMusic")
        {
            Destroy(GameObject.Find("GameMusic"));
            return;
        }

        //MainWorld duplication control
        if (musicInstance == null)
        {
            musicInstance = this; //set instance
            DontDestroyOnLoad(this.gameObject); //used to keep the same object in all scenes
            return;
        }
        Destroy(this.gameObject); //destroy duplicate game music objects when returning to MainWorld

    }
}
