using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateActivated : MonoBehaviour {

    public GameObject door;
    private Animator anim;
    public Animator anim1;
    private AudioSource Aud;

    private void Start()
    {
        Aud = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                Aud.Play();
                Debug.Log("Door opening");
                anim.SetBool("SwitchActivated", true);
                anim1.SetBool("GateOpened", true);
            }
        }
    }
}
