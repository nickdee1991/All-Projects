using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDcard : MonoBehaviour {

    private GameObject player;
    private AudioSource Aud;

    private void Start()
    {
        Aud = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseDown()
    {
        Aud.Play();
        player.GetComponent<Player>().hasIDcard = true;
        Destroy(gameObject);
    }
}
