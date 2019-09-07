using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server45interactable : MonoBehaviour {

    private GameObject player;
    private GameObject L3elevator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        L3elevator = GameObject.Find("L3elevator");
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            L3elevator.GetComponent<FinishL3>().dataRetreived = true;
            Debug.Log("data retrieved, escape via elevator at far end of room");
        }

    }
}
