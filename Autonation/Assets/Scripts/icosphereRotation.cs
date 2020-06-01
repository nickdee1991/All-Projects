using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icosphereRotation : MonoBehaviour {

    public float rotation = 50;
    private AudioSource audioSource;
    public AudioClip[] sound;
    private AudioClip soundPlaying;
    private float timeBetweenSounds;

    public void Start()
    {
        timeBetweenSounds = Random.Range(30, 60);
        StartCoroutine("VoiceClip");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        transform.Rotate (new Vector3(0, Time.deltaTime * rotation, 0));
	}

    IEnumerator VoiceClip()
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
