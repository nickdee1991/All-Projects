using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeRobotInteracting : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip[] sound;
    private AudioClip soundPlaying;
    public float timeBetweenSounds;

    public void Start()
    {
        timeBetweenSounds = Random.Range(5, 12);
        StartCoroutine("InteractingSound");
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
            //yield return new WaitForSeconds(timeBetweenSounds);
        }
    }
}
