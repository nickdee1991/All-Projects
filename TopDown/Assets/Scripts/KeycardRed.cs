﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardRed : MonoBehaviour
{
    private AudioSource audio;
    public GameObject player;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        audio.Play();
        player.GetComponent<Player>().hasKeycard = true;
        Destroy(this.gameObject, 1f);
    }
}
