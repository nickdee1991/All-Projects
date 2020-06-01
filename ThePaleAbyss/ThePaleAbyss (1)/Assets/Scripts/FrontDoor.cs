using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontDoor : MonoBehaviour
{
    private Animator Anim;
    private AudioManager Aud;
    private GameObject player;
    private GameObject sceneBlack;
    private GameObject text;
    private MeshRenderer barricade;
    private bool doorOpen;

    private InteractableManager IntMgr;

    private void Start()
     {
        Aud = FindObjectOfType<AudioManager>();
        doorOpen = false;
        IntMgr = GameObject.FindGameObjectWithTag("InteractableManager").GetComponent<InteractableManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        Anim = GetComponent<Animator>();
        text = GameObject.FindGameObjectWithTag("Text_Holder");
        sceneBlack = GameObject.Find("ToBeBlackened");
        barricade = GameObject.Find("barricade").GetComponent<MeshRenderer>();
     }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(player) && Input.GetKeyDown(KeyCode.E))
        {
            if (doorOpen == false)
            {
                Aud.PlaySound("DoorTry");
                player.GetComponent<Player>().canMoveZ = true;
                text.GetComponentInChildren<Text>().enabled = true;
                text.GetComponentInChildren<Text>().text = "I need to find something to open this (Walk with WASD)";
                text.GetComponentInChildren<TextHolder>().textStart = true;
                Debug.Log(text.GetComponentInChildren<Text>().text);
            }


            if (IntMgr.haslogInt)
            {
                player.GetComponent<Player>().isInteracting = true;
                player.GetComponent<Player>().movementSpeed = 0;
                //Anim.SetBool("haslogInt", true);
                Anim.SetBool("dooropen", true);
                Aud.PlaySound("DoorOpen");
                Debug.Log(text.GetComponentInChildren<Text>().text);

                text.GetComponentInChildren<Text>().enabled = true;
                text.GetComponentInChildren<Text>().text = "That's done it, now to get out of this cold";
                text.GetComponentInChildren<TextHolder>().textStart = true;
                IntMgr.haslogInt = false;
                doorOpen = true;
            }

            if (IntMgr.hasHammerInt && doorOpen)
            {
                player.GetComponent<Player>().isInteracting = true;
                //Anim.SetBool("hashammerInt", true);
                Anim.SetBool("dooropen", false);
                Debug.Log(text.GetComponentInChildren<Text>().text);

                StartCoroutine(DoorClosed());
            }
        }
    }

    public IEnumerator DoorClosed()
    {
        player.GetComponent<Player>().movementSpeed = 0;
        yield return new WaitForSeconds(1);
        Aud.PlaySound("DoorHit");
        yield return new WaitForSeconds(2f);
        Aud.PlaySound("DoorHit");
        yield return new WaitForSeconds(3);     //disable terrain and change camera colour
        Aud.PlaySound("DoorHit");
        sceneBlack.SetActive(false);
        player.GetComponent<Player>().cam.backgroundColor = Color.black;
        player.GetComponentInChildren<ParticleSystem>().Stop(true);
        barricade.enabled = true;
        player.GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(5f);
        player.GetComponent<Player>().movementSpeed = 7.5f;     //return player speed
        IntMgr.secureDoor = true;

        //GetComponent<Text>().enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(player))
        {
            player.GetComponent<Player>().isInteracting = false;
            player.GetComponent<Player>().movementSpeed = 7.5f;
        }
    }

}
