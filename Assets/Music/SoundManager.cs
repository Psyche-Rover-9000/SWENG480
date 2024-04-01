using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public Image musicImage;
    public Image mutedMusic;
    public Image sfxImage;
    public Image mutedSFX;

    public void setVolumeMusic(float sliderValue)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20); //change music volume based on slider

        //if user mutes volume, show muted image
        if (sliderValue <= 0.0001)
        {
            musicImage.gameObject.SetActive(false);
            mutedMusic.gameObject.SetActive(true);
        }
        else
        {
            mutedMusic.gameObject.SetActive(false);
            musicImage.gameObject.SetActive(true);
        }
    }

    public void setVolumeSFX(float sliderValue)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20); //change sfx volume based on slider

        //if user mutes volume, show muted image
        if (sliderValue <= 0.0001)
        {
            sfxImage.gameObject.SetActive(false);
            mutedSFX.gameObject.SetActive(true);
        }
        else
        {
            mutedSFX.gameObject.SetActive(false);
            sfxImage.gameObject.SetActive(true);
        }
    }
}
