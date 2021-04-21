using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private Animator anim;
    private AudioManager aud;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        aud = FindObjectOfType<AudioManager>();
    }

    void Footsteps()
    {
        aud.PlaySound("Footstep");
    }
}
