using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAccessPanel : MonoBehaviour {

    private GameObject player;
    public bool unlocked;
    private Animator anim;
    private AudioSource audioDoor;

	// Use this for initialization
	void Start () {
        unlocked = false;
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        audioDoor = GetComponent<AudioSource>();
    }

    void OnMouseOver()
    {
        if (player.GetComponent<Player>().hasKeycard == true && Input.GetKeyDown(KeyCode.E))
        {
            audioDoor.Play();
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
