using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenEntrance : MonoBehaviour {

    private GameObject player;
    public Animator animator;
    private AudioSource Aud;

    private void Start()
    {
        Aud = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseOver()
    {
        if (player.GetComponent<Player>().hasGardenKey == true && Input.GetKeyDown(KeyCode.E))
        {
            Aud.Play();
            animator.SetBool("OpenGardenDoor", true);
        }
    }
}
