using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3Grate1 : MonoBehaviour {

    public bool grate1open = false;
    private Animator anim;
    private AudioSource audioGrate;

    private void Start()
    {
        audioGrate = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            audioGrate.Play();
            grate1open = true;
            anim.SetBool("grate1open", true);
        }
    }
}
