using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3Grate4 : MonoBehaviour {

    public bool grate4open = false;
    private Animator anim;
    private AudioSource audioGrate;

    private void Start()
    {
        audioGrate = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        audioGrate.Play();
        grate4open = true;
        anim.SetBool("grate4open", true);
    }
}
