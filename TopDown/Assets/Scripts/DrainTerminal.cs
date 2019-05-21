using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainTerminal : MonoBehaviour {

    public Animator animator;
    public ParticleSystem lidSmoke;

	// Use this for initialization
	void Start () {
        lidSmoke.Stop();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            Debug.Log("Drain Terminal Hacked");
            animator.SetBool("DrainLidOpened", true);
            lidSmoke.Play();
        }
    }
}
