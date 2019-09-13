using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Terminal : MonoBehaviour {

    public GameObject satellite;
    public GameObject TerminalController;
    private Collider t_collider;
    public Animator termAnimator;
    public Animator dishAnimator;
    private AudioSource audio;
    private TMPro.TextMeshPro text;
    private string terminalCount;

	// Use this for initialization
	void Start ()
    {
        text = GetComponentInChildren<TMPro.TextMeshPro>();
        text.text = "Bridge Terminal";
        text.color = Color.yellow;
        //terminalsHacked = TerminalController.GetComponent<TerminalsHacked>().terminalsHacked;
        audio = GetComponent<AudioSource>();    
        t_collider = GetComponent<CapsuleCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                t_collider.enabled = !t_collider.enabled;
                audio.Play();
                satellite.GetComponent<AudioSource>().Play();
                //terminalCount = TerminalController.GetComponent<TerminalsHacked>().terminalsHacked;
                text.text = "Terminal Hacked " + TerminalController.GetComponent<TerminalsHacked>().terminalsHacked + "/3";
                text.color = Color.green;
                    //TerminalController.GetComponent<TerminalsHacked>().terminalsHacked;
                TerminalController.GetComponent<TerminalsHacked>().terminalsHacked ++;
                dishAnimator.SetBool("DishRotate", true);
                termAnimator.SetBool("TerminalUnlocked", true);
                Debug.Log("Terminals Hacked = " + TerminalController.GetComponent<TerminalsHacked>().terminalsHacked);
            }
        }
    }
}
