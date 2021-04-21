using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalTrigger : MonoBehaviour
{
    public GameObject[] platforms;
    public GameObject platform;
    public GameObject terminalScreen;

    private bool terminalActivated;

    private AudioManager aud;

    private void Start()
    {
        terminalActivated = false;
        aud = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && terminalActivated == false)
        {
            Debug.Log("Player activated terminal");
            terminalActivated = true;
            aud.PlaySound("TerminalActivate");
            terminalScreen.GetComponent<MeshRenderer>().material.color = (Color.green);
            foreach (var platform in platforms)
            {
                platform.GetComponent<Animator>().SetBool("moving",true);              
            }
        }
    }
}
