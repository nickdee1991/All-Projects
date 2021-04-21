using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    AudioManager aud;

    private void Start()
    {
        aud = FindObjectOfType<AudioManager>();
    }

    void Footstep()
    {
        aud.PlaySound("footstep");
    }
}
