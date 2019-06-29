using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenEntrance : MonoBehaviour {

    private GameObject player;
    public Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseDown()
    {
        if (player.GetComponent<Player>().hasGardenKey == true)
        {
            animator.SetBool("OpenGardenDoor", true);
        }
    }
}
