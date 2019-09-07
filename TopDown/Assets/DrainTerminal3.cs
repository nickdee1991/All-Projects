using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainTerminal3 : MonoBehaviour {

    public bool drainClosed;
    public Animator drainLid;
    private AudioSource Aud;
    public GameObject terminalTextClosed;
    public GameObject terminalTextOpen;

    private void Start()
    {
        Aud = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            terminalTextClosed.SetActive(true);
            terminalTextOpen.SetActive(false);
            Aud.Play();
            drainClosed = true;
            drainLid.SetBool("drainclosed", true);
        }
    }
}
