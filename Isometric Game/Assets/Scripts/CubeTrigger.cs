using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject door;
    private Animator boxTriggerAnim;
    private AudioManager aud;

    private void Start()
    {
        player = GameObject.Find("PlayerBox");
        boxTriggerAnim = GetComponent<Animator>();
        aud = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(player))
        {
            Debug.Log("Door Activated");
            aud.PlaySound("dooropen");
            door.GetComponent<Animator>().SetBool("Open",true);
            boxTriggerAnim.SetBool("Close", true);
        }
    }
}
