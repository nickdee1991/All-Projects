using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDoor : MonoBehaviour
{
    private GameObject player;
    private GameObject playerGfx;
    private CameraHold camHold;
    public Animator anim;
    public Animator animCam;
    private InteractableManager IntMgr;
    private LevelDirector levelDir;

    private void Start()
    {
        IntMgr = GameObject.FindGameObjectWithTag("InteractableManager").GetComponent<InteractableManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerGfx = GameObject.Find("PlayerGraphics");
        levelDir = GameObject.Find("LevelDirector").GetComponent<LevelDirector>();
        anim = playerGfx.GetComponent<Animator>();
        animCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        camHold = FindObjectOfType<CameraHold>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(player) && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Escaped basement");
            StartCoroutine(CameraTransition());
        }
    }

     public IEnumerator CameraTransition()
    {
        animCam.SetBool("CameraFade", true);

        yield return new WaitForSeconds(2.5f);

        IntMgr.inBasement = false;
        IntMgr.inTherapist = true;


        camHold.CameraHolding();
        anim.SetBool("inBasement", false);

        levelDir.movePlayer = false;
        player.GetComponent<Player>().flashlight.enabled = false;
        player.GetComponent<Player>().canMoveZ = false;

        animCam.SetBool("CameraFade", false);
    }
}
