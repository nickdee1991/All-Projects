using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3Grate2 : MonoBehaviour
{

    public bool grate2open = false;
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
            grate2open = true;
            anim.SetBool("grate2open", true);
        }

    }
}



