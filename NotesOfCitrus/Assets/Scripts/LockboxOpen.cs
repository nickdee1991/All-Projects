using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockboxOpen : MonoBehaviour
{
    public Animator anim;
    public GameObject key;
    private GameManager GM;

    private void Start()
    {
        anim = GetComponent<Animator>();
        key.GetComponent<SphereCollider>().enabled = false;
        GM = FindObjectOfType<GameManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && GM.Chisel && GM.Hammer)
        {
            anim.SetBool("LockboxOpen", true);
            GM.Hammer = false;
            GM.Chisel = false;
            key.GetComponent<SphereCollider>().enabled = true;
        }
    }
}
