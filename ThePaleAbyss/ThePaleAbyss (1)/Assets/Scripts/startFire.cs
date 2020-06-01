using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startFire : MonoBehaviour
{
    private AudioManager Aud;
    private GameObject player;
    private InteractableManager IntMgr;
    private GameObject text;
    private GameObject cam;
    private SpriteRenderer bag;
    private Transform fireCam;
    private Animator playerGraphics;
    private bool hasFaded;

    private float cameraFadeTime = 3.5f;

    private void Start()
    {
        hasFaded = false;
        fireCam = GameObject.Find("fireCam").transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        IntMgr = FindObjectOfType<InteractableManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerGraphics = GameObject.Find("PlayerGraphics").GetComponent<Animator>();
        Aud = FindObjectOfType<AudioManager>();
        text = GameObject.FindGameObjectWithTag("Text_Holder");
        bag = GameObject.Find("bag").GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (cam.GetComponent<CameraHold>().cameraIsHolding == false)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(player) && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(text.GetComponentInChildren<Text>().text);
            player.GetComponent<Player>().isInteracting = true;
            Aud.PlaySound("Pickup");

            cam.transform.position = fireCam.transform.position;
            cam.transform.rotation = fireCam.transform.rotation;
            StartCoroutine(cam.GetComponent<CameraHold>().CameraHolding());

            if (!IntMgr.startFire && !IntMgr.breakChair)
            {
                text.GetComponentInChildren<Text>().enabled = true;
                text.GetComponentInChildren<Text>().text = "I bet I could get this started if i had some kindling";
                text.GetComponentInChildren<TextHolder>().textStart = true;
            }

            if (IntMgr.secureDoor && IntMgr.hasHammerInt && IntMgr.breakChair)
            {
                IntMgr.startFire = true;
                cam.transform.position = fireCam.transform.position;
                cam.transform.rotation = fireCam.transform.rotation;

                StartCoroutine(cam.GetComponent<CameraHold>().CameraHolding());
                
                cam.GetComponent<Animator>().SetBool("CameraFade", true);
                StartCoroutine(CameraFireFade());
            }
        }
    }
    public IEnumerator CameraFade()
    {
        yield return new WaitForSeconds(cameraFadeTime);
        cam.GetComponent<Animator>().SetBool("CameraFade", false);

        hasFaded = true;
    }
    public IEnumerator CameraFireFade()
    {

        yield return new WaitForSeconds(cameraFadeTime);

        text.GetComponentInChildren<Text>().enabled = true;
        text.GetComponentInChildren<Text>().text = "The fire is burning strong. Hopefully I can keep it lit";
        text.GetComponentInChildren<TextHolder>().textStart = true;

        IntMgr.startFire = true;
        GetComponentInChildren<ParticleSystem>().Play();
        GameObject.Find("fireLight").GetComponent<Light>().enabled = true;
        playerGraphics.SetBool("noCoat", true);
        bag.enabled = true;
        cam.GetComponent<Animator>().SetBool("CameraFade", false);
        
        hasFaded = true;
    }
}
