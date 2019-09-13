using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server45interactable : MonoBehaviour {

    private GameObject player;
    public GameObject L3elevator;
    private AudioSource Aud;
    private Light ServerLight;

    private void Start()
    {
        ServerLight = GetComponentInChildren<Light>();
        Aud = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        ServerLight.color = Color.red;
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ServerLight.color = Color.green;
            Aud.Play();
            L3elevator.GetComponent<FinishL3>().dataRetreived = true;
            Debug.Log("data retrieved, escape via elevator at far end of room");
        }

    }
}
