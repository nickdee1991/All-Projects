using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeInserted : MonoBehaviour {

    private Animator anim;
    public GameObject tapeObj;
    public GameObject insertedTape;
    private AudioSource Aud;

    private void Start()
    {
        Aud = GetComponent<AudioSource>();
        insertedTape.SetActive(false);
        anim = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        if (tapeObj.GetComponent<TapeCollected>().tapeCollected == true)
        {
            insertedTape.SetActive(true);
            anim.SetBool("TapeInserted", true);
            //play recording
            Aud.Play();
        }
    }
}
