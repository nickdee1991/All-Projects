using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoors : MonoBehaviour
{

    public AudioManager audioMgr;
    private Animator anim;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        audioMgr = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void ElevatorDoor()
    {
            audioMgr.PlaySound("ElevatorOpen");
            anim.SetBool("ElevatorOpen", true);
    }

    private void OnTriggerEnter(Collider other)
    {
    if (other.gameObject.Equals(player))
    {
        ElevatorDoor();
    }
    }
}
