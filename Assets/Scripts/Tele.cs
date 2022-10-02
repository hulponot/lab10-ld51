using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tele : MonoBehaviour
{

    public AudioClip rocketBeep;
    public AudioClip rocketBeeeeeeeeeep;
    public AudioClip rocketup;
    public AudioSource audioSource;

    private GG gg;
    private void Start()
    {
        gg = FindObjectOfType<GG>();
        audioSource.volume = Settings.EffectsVolume;
    }
    public void FadedOut(){
        Debug.Log("faded out");
        gg.FadeOut();
    }
    public void FadedIn(){
        Debug.Log("faded in");
        gg.FadeIn();
    }

    public void playRocketBeep(){
        audioSource.clip = rocketBeep;
        audioSource.Play();
    }
    public void playRocketBeeeeeeep(){
        audioSource.clip = rocketBeeeeeeeeeep;
        audioSource.Play();
    }
    public void playRocketup(){
        audioSource.clip = rocketup;
        audioSource.Play();
    }
}
