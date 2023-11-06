using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public void MusicOn()
    {
        AudioListener.volume = 1;
    }

    public void MusicOff()
    {
       AudioListener.volume = 0;
    }
}
