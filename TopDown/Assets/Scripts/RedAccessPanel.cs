using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAccessPanel : MonoBehaviour {

    private GameObject player;
    public bool unlocked;
    private Animator anim;

	// Use this for initialization
	void Start () {
        unlocked = false;
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
	}

    void OnMouseDown()
    {
        if (player.GetComponent<Player>().hasKeycard == true)
        {
            anim.SetBool("AutodoorOpen", true);
            unlocked = true;
            Debug.Log(player.GetComponent<Player>().hasKeycard);
        }else
        {
            Debug.Log("No red keycard");
            //play sound "EEERRORR"
        }
    }
}
