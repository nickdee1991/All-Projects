using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEnteredCheck : MonoBehaviour {

    private GameObject player;
    public bool ElevatorEntered = false;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            ElevatorEntered = true;
        }
    }

}
