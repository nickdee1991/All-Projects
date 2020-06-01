using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hasHammerInt : MonoBehaviour
{
    private AudioManager Aud;
    private GameObject player;
    private InteractableManager IntMgr;
    private GameObject text;
    private GameObject cam;
    private Transform hammerCam;
    public MeshRenderer handle;
    public MeshRenderer hammer;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        hammerCam = GameObject.Find("hammerCam").transform;
        IntMgr = GameObject.FindGameObjectWithTag("InteractableManager").GetComponent<InteractableManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        Aud = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        text = GameObject.FindGameObjectWithTag("Text_Holder");
    }
    private void Update()
    {
        if (cam.GetComponent<CameraHold>().cameraIsHolding == false && IntMgr.hasHammerInt == true)
        {
            hammer.enabled = false;
            handle.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(player) && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(text.GetComponentInChildren<Text>().text);
            player.GetComponent<Player>().isInteracting = true;
            cam.transform.position = hammerCam.transform.position;
            cam.transform.rotation = hammerCam.transform.rotation;

            StartCoroutine(cam.GetComponent<CameraHold>().CameraHolding());

            Aud.PlaySound("Pickup");
            IntMgr.hasHammerInt = true;

            text.GetComponentInChildren<Text>().enabled = true;
            text.GetComponentInChildren<Text>().text = "Good, a hammer and nails. Now to get this door closed.";
            text.GetComponentInChildren<TextHolder>().textStart = true;
        }
    }
}
