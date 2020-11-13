using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    private AudioManager aud;
    private bool audioPlayed;
    public string audioName;

    private void Start()
    {
        audioPlayed = false;
        aud = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (audioPlayed == false)
        {
            aud.PlaySound(audioName);
            audioPlayed = true;
        }

    }
}
