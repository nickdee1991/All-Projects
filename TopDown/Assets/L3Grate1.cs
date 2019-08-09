using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3Grate1 : MonoBehaviour {

    public bool grate1open = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        grate1open = true;
        anim.SetBool("grate1open", true);
    }
}
