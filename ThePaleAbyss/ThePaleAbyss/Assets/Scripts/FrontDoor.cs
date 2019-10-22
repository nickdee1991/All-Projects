using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoor : MonoBehaviour
{
    private GameObject player;
    private Animator Anim;
    private AudioSource Aud;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Anim = GetComponent<Animator>();
        Aud = GetComponent<AudioSource>();
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Anim.SetBool("dooropen", true);
            Aud.Play();
        }
    }

}
