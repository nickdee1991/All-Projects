using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishL3 : MonoBehaviour {

    private GameObject player;
    public BoxCollider playerCol;
    private Animator anim;
    public BoxCollider EndGame;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        //playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
	}

    public void OnTriggerEnter(Collider other)
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<SphereCollider>());
        if (other.GetComponent<Collider>() == playerCol)
        {
            anim.SetTrigger("L3ElevatorUp");
        }
        if (other.GetComponent<BoxCollider>() == EndGame)
        {
            Debug.Log("End Game");
        }

    }
}
