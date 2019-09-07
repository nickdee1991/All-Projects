using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCabinetExplosion : MonoBehaviour {

    private GameObject fusebox;
    private Animator anim;
    private GameObject fireEffect;
    private AudioSource explosion;
    public bool isDestroyed;

	// Use this for initialization
	void Start () {
        fireEffect.SetActive(false);
        fusebox = GameObject.FindGameObjectWithTag("Fusebox");
        anim = GetComponent<Animator>();
        fireEffect = GameObject.FindGameObjectWithTag("fireEffect");
        explosion = GetComponent<AudioSource>();
        isDestroyed = true;
    }

    private void Update()
    {
        //if (fusebox.GetComponent<fuseboxinteractable>().fuseBoxDestroyed == true)
        //{
            //anim.SetBool("serverroomdestroyed", true);
            //fireEffect.SetActive(true);
            //explosion.Play();
        //}
    }
}
