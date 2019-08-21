using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateGrate : MonoBehaviour {

    private Animator anim;
    private AudioSource audioGrate;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        audioGrate = GetComponent<AudioSource>();

    }
    private void OnMouseDown()
    {
        audioGrate.Play();
        Debug.Log("Grate Opening");
        anim.SetBool("ActivateGrate", true);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioGrate.Play();
            anim.SetBool("ActivateGrate(Bottom)", true);
        }
    }
}
