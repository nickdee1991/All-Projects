using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorEnteredCheck : MonoBehaviour {

    private GameObject player;
    private Animator elevatoranimator;
    public bool ElevatorEntered = false;
    public GameObject terminal;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        elevatoranimator = GetComponent<Animator>();
        Physics.IgnoreCollision(player.GetComponent<SphereCollider>(), GetComponent<BoxCollider>());
    }

    public void OnTriggerStay(Collider other)
    {
        //Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), player.GetComponent<SphereCollider>());
        if (other.gameObject == player)
        {
            player.transform.parent = transform;
            //Debug.Log("Player in elevator " + ElevatorEntered);
            ElevatorEntered = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        player.transform.parent = null;
        //Debug.Log("Player in elevator " + ElevatorEntered);
        ElevatorEntered = false;
        //terminal.GetComponent<ElevatorController>().ElevatorDescended = false;
        elevatoranimator.SetBool("ElevatorAscend", false);
        elevatoranimator.SetBool("ElevatorDescend", false);
    }

}
