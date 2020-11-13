using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private AudioManager Aud;

        private void Start()
    {
        Aud = FindObjectOfType<AudioManager>();
    }


    public void Footstep()
    {
        Aud.PlaySound("Footstep");
    }
}
