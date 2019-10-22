using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerOfficeDoor : MonoBehaviour {

    private GameObject player;
    private Animator anim;
    private AudioSource Aud;

	// Use this for initialization
	void Start ()
    {
        Aud = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
	}

    private void OnMouseOver()
    {
        if (player.GetComponent<Player>().hasIDcard == true && Input.GetKeyDown(KeyCode.E))
        {
            Aud.Play();
            anim.SetBool("DoorOpen", true);
        }
    }
}
