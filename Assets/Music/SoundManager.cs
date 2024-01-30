using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    //creates an instance of the game sound
    private static Music musicInstance = null;
    private void Awake()
    {
        if (musicInstance == null)
        {
            musicInstance = this; //set instance
            DontDestroyOnLoad(this.gameObject); //used to keep the same object in all scenes
            return;
        }
        Destroy(this.gameObject); //destroy duplicate game music objects when returning to MainWorld

    }

    public void MusicOn()
    {
        audioMixer.SetFloat("MusicVolume", 0);
    }

    public void MusicOff()
    {
        audioMixer.SetFloat("MusicVolume", -80);
    }

    public void SFXOn()
    {
        audioMixer.SetFloat("SFXVolume", 0);
    }

    public void SFXOff()
    {
        audioMixer.SetFloat("SFXVolume", -80);
    }
}
