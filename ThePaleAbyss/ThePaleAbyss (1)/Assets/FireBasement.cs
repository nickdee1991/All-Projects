using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBasement : MonoBehaviour
{
    private AudioManager Aud;
    private GameObject player;
    private InteractableManager IntMgr;
    private ParticleSystem.MainModule Fire;

    private void Start()
    {
        IntMgr = FindObjectOfType<InteractableManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        Aud = FindObjectOfType<AudioManager>();
        Fire = GetComponentInChildren<ParticleSystem>().main;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(player) && Input.GetKeyDown(KeyCode.E) && IntMgr.itemCollected)
        {
            IntMgr.chosenEnding = InteractableManager.Ending.ItemDestroyed;

            Fire.startLifetime = 1f;
            Fire.startColor = Color.red;
            Aud.PlaySound("Fire");
        }
    }
}
