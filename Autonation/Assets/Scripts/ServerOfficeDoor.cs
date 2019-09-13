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

    private void OnMouseOver()
    {
        if (player.GetComponent<Player>().hasIDcard == true && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("DoorOpen", true);
        }
    }
}
