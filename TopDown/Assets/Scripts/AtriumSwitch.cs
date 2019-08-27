using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtriumSwitch : MonoBehaviour {

    private GameObject player;
    public GameObject water;
    public Animator anim1;
    public Animator anim2;
    public bool atriumSwitchActivated;
    public AudioSource waterDrain;

        private void Start()
    {
        atriumSwitchActivated = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void OnMouseDown()
    {
            atriumSwitchActivated = true;
            Debug.Log("Atrium Trigger Activated");
            anim1.SetBool("waterlower", true);
            anim2.SetBool("atriumswitch", true);
            waterDrain.Play();
    }
}
