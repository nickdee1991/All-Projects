using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Terminal : MonoBehaviour {

    public GameObject satellite;
    public GameObject TerminalController;
    public float roty = 90;
    private Collider t_collider;
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
                satellite.transform.Rotate(0, roty, 0, 0);
                t_collider.enabled = !t_collider.enabled;
                Debug.Log("Terminals Hacked = " + TerminalController.GetComponent<TerminalsHacked>().terminalsHacked);
            }
        }
    }
}
