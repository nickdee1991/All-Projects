using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    public AudioManager audioMgr;
    // Start is called before the first frame update
    void Start()
    {
        audioMgr = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void Footstep1()
    {
        audioMgr.PlaySound("Footstep1");
    }

    public void Footstep2()
    {
        audioMgr.PlaySound("Footstep2");
    }

    public void Footstep3()
    {
        audioMgr.PlaySound("Footstep3");
    }
}
