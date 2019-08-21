using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtriumSwitch : MonoBehaviour {

    private GameObject player;
    public GameObject water;
    private Animator anim;
    public bool atriumSwitchActivated;

        private void Start()
    {
        atriumSwitchActivated = false;
        player = GameObject.FindGameObjectWithTag("Player");
        anim = water.GetComponent<Animator>();
    }
    public void OnMouseDown()
    {
            atriumSwitchActivated = true;
            Debug.Log("Atrium Trigger Activated");
            anim.SetBool("waterlower", true);
    }
}
