using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrainTerminal : MonoBehaviour {

    public Animator animator;
    public ParticleSystem lidSmoke;
    private TMPro.TextMeshPro text;
    private AudioSource audioKeypress;
    public bool drainLidOpened;

    // Use this for initialization
    void Start () {
        drainLidOpened = false;
        lidSmoke.Stop();
        text = GetComponentInChildren<TMPro.TextMeshPro>();
        text.text = "ROOF \n VENTILATION \n CLOSED";
        text.color = Color.yellow;
        audioKeypress = GetComponent<AudioSource>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            audioKeypress.Play();
            text.text = "ROOF \n VENTILATION \n OPEN";
            text.color = Color.green;
            Debug.Log("Drain Terminal Hacked");
            animator.SetBool("DrainLidOpened", true);
            lidSmoke.Play();
            drainLidOpened = true;
        }
    }
}
