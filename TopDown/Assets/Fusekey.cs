using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusekey : MonoBehaviour {

    private AudioSource Aud;
    private GameObject player;
    public GameObject fuseBox;


    private void Start()
    {
        Aud = GetComponent<AudioSource>();
        fuseBox.GetComponent<fuseboxinteractable>().fuseKeyaquired = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("fusekey picked up");
            Aud.Play();
            fuseBox.GetComponent<fuseboxinteractable>().fuseKeyaquired = true;
            gameObject.SetActive(false);
        }
    }
}
