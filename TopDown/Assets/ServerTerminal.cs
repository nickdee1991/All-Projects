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

    public Animator server45anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseDown()
    {
        if(player.GetComponent<Player>().hasIDcard == true)
        {
            Audgranted.Play();
            server45anim.SetBool("server45open", true);
            Debug.Log("hasIDcard");
            objComplete.SetActive(true);
            objNotComplete.SetActive(false);
            server45.GetComponent<Renderer>().material.color = Color.green;
        }

        if (player.GetComponent<Player>().hasIDcard == false)
        {
            Auddenied.Play();
            Debug.Log("doesnothaveIDcard");
            objNotComplete.SetActive(true);
        }
    }
}
