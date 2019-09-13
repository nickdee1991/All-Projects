using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3Grate5 : MonoBehaviour {

    public bool grate5open = false;
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
            grate5open = true;
            anim.SetBool("grate5open", true);
        }
    }
}
