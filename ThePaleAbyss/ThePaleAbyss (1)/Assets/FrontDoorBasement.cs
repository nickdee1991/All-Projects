using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoorBasement : MonoBehaviour
{
    private GameObject player;
    private Animator anim;
    private AudioManager Aud;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        Aud = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(player) && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("dooropen",true);
            Aud.PlaySound("DoorOpen");
        }
    }
}
