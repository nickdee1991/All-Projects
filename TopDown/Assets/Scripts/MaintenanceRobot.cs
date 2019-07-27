using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintenanceRobot : MonoBehaviour {

    public GameObject text;
    public GameObject text2;
    private GameObject player;
    public GameObject terminal;
    private AudioSource audioClip;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioClip = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        audioClip.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        transform.LookAt(player.transform);       
        if (other.gameObject == player && terminal.GetComponent<ReceptionDoorTerminal>().ReceptionDoorOpen == false)
        {
            text.gameObject.SetActive(true);
        } else if (other.gameObject == player && terminal.GetComponent<ReceptionDoorTerminal>().ReceptionDoorOpen == true)
        {
            Debug.Log("Hey nice work chief! Lets go find that keycard");
            text.gameObject.SetActive(false);
            text2.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.LookAt(terminal.transform);

        if (other.gameObject == player)
        {
            text.gameObject.SetActive(false);
            text2.gameObject.SetActive(false);
        }
    }
}
