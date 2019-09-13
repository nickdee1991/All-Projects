using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3Grate3 : MonoBehaviour {

    public bool grate3open = false;
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
            grate3open = true;
            anim.SetBool("grate3open", true);
        }
    }
}
