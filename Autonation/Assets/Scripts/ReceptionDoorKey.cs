using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionDoorKey : MonoBehaviour {

    public bool fusePickedUp = false;
    private AudioSource Aud;

    private void Start()
    {
        Aud = GetComponent<AudioSource>();
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            fusePickedUp = true;
            Debug.Log("Reception Door Fuse Picked Up");
            gameObject.SetActive(false);
            Aud.Play();
        }
    }
}
