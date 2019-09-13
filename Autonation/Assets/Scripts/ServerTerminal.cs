using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerTerminal : MonoBehaviour {

    public GameObject server45;
    public GameObject objComplete;
    public GameObject objNotComplete;
    private GameObject player;
    public AudioSource Audgranted;
    public AudioSource Auddenied;
    public bool serverComputerHacked;

    public Animator server45anim;

    private void Start()
    {
        serverComputerHacked = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnMouseOver()
    {
        if(player.GetComponent<Player>().hasIDcard == true && Input.GetKeyDown(KeyCode.E))
        {
            Audgranted.Play();
            server45anim.SetBool("server45open", true);
            Debug.Log("hasIDcard");
            objComplete.SetActive(true);
            objNotComplete.SetActive(false);
            server45.GetComponent<Renderer>().material.color = Color.green;
        }

        if (player.GetComponent<Player>().hasIDcard == false && Input.GetKeyDown(KeyCode.E))
        {
            Auddenied.Play();
            Debug.Log("doesnothaveIDcard");
            objNotComplete.SetActive(true);
            serverComputerHacked = true;
        }
    }
}
