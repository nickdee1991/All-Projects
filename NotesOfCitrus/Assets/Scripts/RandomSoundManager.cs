using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundManager : MonoBehaviour {

    private AudioSource audioSource; // add audioSource to same gameobject
    public AudioClip[] sound;
    private AudioClip soundPlaying;
    public bool playOnAwake = false; // check to enable looping random sounds on startup 
    private float timeBetweenSounds;
    public float SoonestTime;
    public float FurthestTime;

    public void Start()
    {
        if (playOnAwake)
        {
            StartCoroutine("InteractingSound");
        }

        audioSource = GetComponent<AudioSource>();
    }


    IEnumerator InteractingSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSounds);
            int index = Random.Range(0, sound.Length);
            soundPlaying = sound[index];
            audioSource.clip = soundPlaying;
            audioSource.Play();
        }
    }
}
