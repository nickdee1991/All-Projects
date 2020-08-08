using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AudioSource audS;

    private void Start()
    {
        audS = GetComponent<AudioSource>();
        audS.Play();
    }
}
