using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintenanceRobotL2 : MonoBehaviour {

    public GameObject grate;
    public GameObject text;
    public GameObject text2;
    private GameObject player;
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
        if (other.gameObject == player)
        {
            text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            text.gameObject.SetActive(false);
            //transform.LookAt(grate.transform);
        }
    }
}
