using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private GameManager GM;
    private GameObject player;
    private MeshRenderer ItemGraphic;
    private AudioManager Aud;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        ItemGraphic = GetComponent<MeshRenderer>();
        Aud = FindObjectOfType<AudioManager>();
    }
}
