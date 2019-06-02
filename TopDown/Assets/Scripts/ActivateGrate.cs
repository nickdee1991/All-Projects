using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGrate : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
    private void OnMouseDown()
    {
        Debug.Log("Grate Opening");
        anim.SetBool("ActivateGrate", true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("ActivateGrate(Bottom)", true);
        }
    }
}
