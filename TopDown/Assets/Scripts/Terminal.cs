using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Terminal : MonoBehaviour {

    public GameObject satellite;
    public GameObject TerminalController;
    private Collider t_collider;
    public Animator termAnimator;
    public Animator dishAnimator;
    //private float speed = 8;

	// Use this for initialization
	void Start ()
    {
    //terminalsHacked = TerminalController.GetComponent<TerminalsHacked>().terminalsHacked;
    t_collider = this.GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                TerminalController.GetComponent<TerminalsHacked>().terminalsHacked += 1;
                dishAnimator.SetBool("DishRotate", true);
                t_collider.enabled = !t_collider.enabled;
                this.termAnimator.SetBool("TerminalUnlocked", true);
                Debug.Log("Terminals Hacked = " + TerminalController.GetComponent<TerminalsHacked>().terminalsHacked);
            }
        }
    }
}
