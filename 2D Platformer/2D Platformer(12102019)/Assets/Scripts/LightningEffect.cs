using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class LightningEffect : MonoBehaviour
{

    private AudioSource audioSource;
    private RawImage LightningMask;
    public AudioClip[] sound;
    private AudioClip soundPlaying;
    public float timeBetweenStrikes;



    public void Start()
    {
        LightningMask = GameObject.Find("lightningGraphics").GetComponent<RawImage>(); ;
        LightningMask.enabled = false;
        timeBetweenStrikes = Random.Range(12, 24);
        StartCoroutine("Lightning");
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator Lightning()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStrikes);
            int index = Random.Range(0, sound.Length);
            soundPlaying = sound[index];
            audioSource.clip = soundPlaying;
            audioSource.Play();
            LightningMask.enabled = true;
            yield return new WaitForSeconds(0.1f);
            LightningMask.enabled = false;

            //yield return new WaitForSeconds(timeBetweenSounds);
        }
    }
}
