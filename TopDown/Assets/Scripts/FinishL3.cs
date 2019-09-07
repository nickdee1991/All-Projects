using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishL3 : MonoBehaviour {

    private GameObject player;
    public BoxCollider playerCol;
    private Animator anim;
    public BoxCollider EndGame;
    public bool dataRetreived;

	// Use this for initialization
	void Start () {
        dataRetreived = false;
        player = GameObject.FindGameObjectWithTag("Player");
        //playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
	}

    public void OnTriggerEnter(Collider other)
    {
        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<SphereCollider>());
        if (other.GetComponent<Collider>() == playerCol && dataRetreived == true)
        {
            anim.SetTrigger("L3ElevatorUp");
        }
        if (other.GetComponent<BoxCollider>() == EndGame)
        {
            Debug.Log("End Game");
        }

    }
}
