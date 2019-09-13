using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionDoorTerminal : MonoBehaviour {

    public GameObject door;
    public GameObject fuseTerminal;
    public GameObject floatingText;
    public Animator animator;
    public bool ReceptionDoorOpen = false;
    private AudioSource Aud;

    private void Start()
    {
        Aud = GetComponent<AudioSource>();
    }

    // Use this for initialization
    private void OnMouseEnter()
    {
        floatingText.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        floatingText.gameObject.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (fuseTerminal.GetComponent<ReceptionDoorKey>().fusePickedUp == true)
            {
                Debug.Log("Fuse placed, door opening");
                //door.transform.Translate(Vector3.up * Time.deltaTime * 200f);
                animator.SetBool("ReceptionDoorOpen", true);
                ReceptionDoorOpen = true;
                Aud.Play();
            }
        }
    }
}
