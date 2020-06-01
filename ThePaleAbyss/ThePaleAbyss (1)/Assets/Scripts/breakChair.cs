using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class breakChair : MonoBehaviour
{
    private AudioManager Aud;
    private Animator anim;
    private GameObject player;
    private InteractableManager IntMgr;
    private GameObject text;
    private GameObject cam;
    private Transform chairCam;

    private void Start()
    {
        anim = GetComponent<Animator>();
        chairCam = GameObject.Find("chairCam").transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        IntMgr = GameObject.FindGameObjectWithTag("InteractableManager").GetComponent<InteractableManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        Aud = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        text = GameObject.FindGameObjectWithTag("Text_Holder");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(player) && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(text.GetComponentInChildren<Text>().text);
            player.GetComponent<Player>().isInteracting = true;
            cam.transform.position = chairCam.transform.position;
            cam.transform.rotation = chairCam.transform.rotation;
            anim.SetBool("breakChair", true);
            StartCoroutine(cam.GetComponent<CameraHold>().CameraHolding());

            Aud.PlaySound("Pickup");
            IntMgr.breakChair = true;

            text.GetComponentInChildren<Text>().enabled = true;
            text.GetComponentInChildren<Text>().text = "I can use this as kindling to start the fire.";
            text.GetComponentInChildren<TextHolder>().textStart = true;
        }
    }
}
