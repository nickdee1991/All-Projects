using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logInt : MonoBehaviour
{
    private AudioManager Aud;
    private GameObject player;
    public InteractableManager IntMgr;
    private GameObject text;
    private GameObject cam;
    private Transform logCam;

    private void Start()
    {
        logCam = GameObject.Find("logCamPos").transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        IntMgr = GameObject.FindGameObjectWithTag("InteractableManager").GetComponent<InteractableManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        Aud = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        text = GameObject.FindGameObjectWithTag("Text_Holder");
    }
    private void Update()
    {
        if (cam.GetComponent<CameraHold>().cameraIsHolding == true && IntMgr.haslogInt == true)
        {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(player) && Input.GetKeyDown(KeyCode.E))
        {

            cam.transform.position = logCam.transform.position;
            cam.transform.rotation = logCam.transform.rotation;
            StartCoroutine(cam.GetComponent<CameraHold>().CameraHolding());

            Debug.Log(text.GetComponentInChildren<Text>().text);
            player.GetComponent<Player>().isInteracting = true;
            Aud.PlaySound("Pickup");

            text.GetComponentInChildren<Text>().enabled = true;
            text.GetComponentInChildren<Text>().text = "One of these should be able to break the lock";
            text.GetComponentInChildren<TextHolder>().textStart = true;
            IntMgr.haslogInt = true;
        }
    }
}
