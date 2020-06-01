using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openWoodHatch : MonoBehaviour
{
    private Animator anim;
    private AudioManager Aud;
    private GameObject cam;
    private Transform hatchCam;
    private Transform faceCam;
    private GameObject text;
    private InteractableManager IntMgr;
    private LevelDirector levelDir;
    private GameObject player;
    private bool hasFaded;

    private float cameraFadeTime = 4f;



    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        Aud = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        IntMgr = FindObjectOfType<InteractableManager>();
        hatchCam = GameObject.Find("hatchCam").transform;
        faceCam = GameObject.Find("faceCam").transform;
        text = GameObject.FindGameObjectWithTag("Text_Holder");
        player = GameObject.FindGameObjectWithTag("Player");
        levelDir = GameObject.Find("LevelDirector").GetComponent<LevelDirector>();
        hasFaded = false;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(player) && Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.P))
        {

            Debug.Log(text.GetComponentInChildren<Text>().text);
            player.GetComponent<Player>().isInteracting = true;
            cam.transform.position = hatchCam.transform.position;
            cam.transform.rotation = hatchCam.transform.rotation;
            StartCoroutine(cam.GetComponent<CameraHold>().CameraHolding());

            anim.SetBool("HatchOpen", true);
            Aud.PlaySound("Pickup");
            IntMgr.pryHatch = true;
        }

        if (other.gameObject.Equals(player) && Input.GetKeyDown(KeyCode.E) && IntMgr.startFire && IntMgr.pryHatch || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.T))
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                IntMgr.haslogInt = true;
                IntMgr.secureDoor = true;
                IntMgr.breakChair = true;
                IntMgr.startFire = true;
                IntMgr.pryHatch = true;
            }
            
            if (Input.GetKeyDown(KeyCode.T))
            {
                IntMgr.haslogInt = true;
                IntMgr.secureDoor = true;
                IntMgr.breakChair = true;
                IntMgr.startFire = true;
                IntMgr.pryHatch = true;

                IntMgr.inTherapist = true;
                return;
            }

            cam.transform.position = hatchCam.transform.position;
            cam.transform.rotation = hatchCam.transform.rotation;
            StartCoroutine(cam.GetComponent<CameraHold>().CameraHolding());

            text.GetComponentInChildren<Text>().enabled = true;
            text.GetComponentInChildren<Text>().text = "I'm not braving that storm again";
            text.GetComponentInChildren<TextHolder>().textStart = true;
            Debug.Log(text.GetComponentInChildren<Text>().text);

            cam.GetComponent<Animator>().SetBool("CameraFade", true);

            StartCoroutine(CameraHatchFade());
        }
    }

    public IEnumerator CameraHatchFade()
    {
        yield return new WaitForSeconds(1);
        cam.transform.position = faceCam.transform.position;
        cam.transform.rotation = faceCam.transform.rotation;

        text.GetComponentInChildren<Text>().enabled = true;
        text.GetComponentInChildren<Text>().text = "But maybe there's another way out";
        text.GetComponentInChildren<TextHolder>().textStart = true;
        Debug.Log(text.GetComponentInChildren<Text>().text);

        yield return new WaitForSeconds(cameraFadeTime);
        IntMgr.inBasement = true;
        cam.GetComponent<Animator>().SetBool("CameraFade", false);
        player.GetComponent<Player>().flashlight.enabled = true;
        hasFaded = true;
    }
}

        
    

