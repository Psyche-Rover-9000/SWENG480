using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

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
