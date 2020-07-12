using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundManager : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip[] sound;
    private AudioClip soundPlaying;
    private float timeBetweenSounds;
    public float SoonestTime;
    public float FurthestTime;

    public void Start()
    {
        timeBetweenSounds = Random.Range(SoonestTime, FurthestTime);
        //StartCoroutine("InteractingSound");
        audioSource = GetComponent<AudioSource>();
    }


    IEnumerator InteractingSound()
    {
            yield return new WaitForSeconds(timeBetweenSounds);
            int index = Random.Range(0, sound.Length);
            soundPlaying = sound[index];
            audioSource.clip = soundPlaying;
            audioSource.Play();
            //yield return new WaitForSeconds(timeBetweenSounds);
    }
}
