using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(PlayerControllerRigidBody))]
public class AnimController : MonoBehaviour {

    public Animator Player;

	// Use this for initialization
	void Start ()
    {
        Player = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        bool Throw = Input.GetMouseButtonDown(0);

        Player.SetBool("Throw", Throw);

        float Run = Input.GetAxis("Vertical");

        

        Player.SetFloat("InputX", Run);
    }
}
