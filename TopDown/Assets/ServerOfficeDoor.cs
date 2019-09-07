using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerOfficeDoor : MonoBehaviour {

    private GameObject player;
    private Animator anim;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
	}

    private void OnMouseDown()
    {
        if (player.GetComponent<Player>().hasIDcard == true)
        {
            anim.SetBool("DoorOpen", true);
        }
    }
}
