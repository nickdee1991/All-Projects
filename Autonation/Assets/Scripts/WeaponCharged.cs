using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCharged : MonoBehaviour {

    private GameObject player;
    private AudioSource Aud;
    public GameObject taserLED;

        private void Start()
    {
        Aud = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(player) && Input.GetKeyDown(KeyCode.E))
        {
            Aud.Play();
            player.GetComponent<Player>().weaponCharged = true;
            taserLED.GetComponent<Renderer>().material.color = Color.green;
            Debug.Log("weapon charged");
        }
    }
}
