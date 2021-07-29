using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    private AudioSource Aud;

    private void Start()
    {
        Aud = GetComponent<AudioSource>();
    }

    void Footstep()
    {
        Aud.Play();
    }
}
