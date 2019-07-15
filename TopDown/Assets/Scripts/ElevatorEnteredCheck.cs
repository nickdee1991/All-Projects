using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEnteredCheck : MonoBehaviour {

    private GameObject player;
    public bool ElevatorEntered = false;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(player.GetComponent<SphereCollider>(), this.GetComponent<BoxCollider>());
    }

    public void Update()
    {
        //ElevatorEntered = false;
    }

    public void OnTriggerStay(Collider other)
    {
        //Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), player.GetComponent<SphereCollider>());
        if (other.gameObject == player)
        {
            Debug.Log("Player in elevator " + ElevatorEntered);
            ElevatorEntered = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Player in elevator " + ElevatorEntered);
        ElevatorEntered = false;
    }

}
