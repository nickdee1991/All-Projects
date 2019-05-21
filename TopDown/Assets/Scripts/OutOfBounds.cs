using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OutOfBounds : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<Player>().health = 0;
        }
    }
}
