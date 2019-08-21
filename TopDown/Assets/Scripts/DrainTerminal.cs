using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainTerminal : MonoBehaviour {

    public Animator animator;
    public ParticleSystem lidSmoke;
    private TextMesh text;
    private AudioSource audioKeypress;

    // Use this for initialization
    void Start () {
        lidSmoke.Stop();
        text = GetComponentInChildren<TextMesh>();
        text.text = "Roof Ventilation";
        text.color = Color.yellow;
        audioKeypress = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            audioKeypress.Play();
            text.text = "Roof vent is open";
            text.color = Color.green;
            Debug.Log("Drain Terminal Hacked");
            animator.SetBool("DrainLidOpened", true);
            lidSmoke.Play();
        }
    }
}
