using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockboxOpen : MonoBehaviour
{
    public Animator anim;
    private AudioManager Aud;
    public GameObject key;
    private GameManager GM;

    private void Start()
    {
        Aud = FindObjectOfType<AudioManager>();
        anim = GetComponent<Animator>();
        key.GetComponent<SphereCollider>().enabled = false;
        GM = FindObjectOfType<GameManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && GM.Chisel && GM.Hammer)
        {
            Aud.PlaySound("LockboxOpen");
            anim.SetBool("LockboxOpen", true);
            GM.Hammer = false;
            GM.Chisel = false;
            GetComponent<SphereCollider>().enabled = false;
            key.GetComponent<SphereCollider>().enabled = true;
        }
        else if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Aud.PlaySound("LockboxClosed");
        }
    }
}
