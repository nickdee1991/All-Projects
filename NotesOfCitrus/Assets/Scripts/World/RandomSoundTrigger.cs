using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundTrigger : MonoBehaviour {

    public AudioSource audioSource; // add audioSource to same gameobject
    public AudioClip[] sound;
    private AudioClip soundPlaying;
    public bool playOnAwake; // check to enable looping random sounds on startup 

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (playOnAwake)
        {
            StartCoroutine("InteractingSound");
        }
    }


    IEnumerator InteractingSound()
    {
        int index = Random.Range(0, sound.Length);
        soundPlaying = sound[index];
        audioSource.clip = soundPlaying;
        audioSource.Play();
        yield return null;
    }
}
