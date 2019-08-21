using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtriumGrate : MonoBehaviour {

    public bool atriumgrateopen = false;
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
        atriumgrateopen = true;
        anim.SetBool("atriumgrateopen", true);
    }
}
