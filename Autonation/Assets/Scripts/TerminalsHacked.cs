using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalsHacked : MonoBehaviour {

    public int terminalsHacked;
    public Animator animator;

    // Use this for initialization
    void Start () {
        terminalsHacked = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (terminalsHacked >= 4)
        {
            animator.SetBool("BridgeActivated", true);
        }
    }
}
