using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBarrier : MonoBehaviour
{
    private AudioManager Aud;

    private void Start()
    {
        Aud = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Aud.PlaySound("IceCrack");
            Debug.Log("player has entered ice barrier");
        }
    }
}
