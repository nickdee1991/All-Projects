using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour {

    private GameObject fuseBox;
    private Animator anim;

    private void Start()
    {
        fuseBox = GameObject.FindGameObjectWithTag("Fusebox");
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (fuseBox.GetComponent<fuseboxinteractable>().fuseBoxDestroyed == true)
        {
            anim.SetBool("finaldooropen", true);
        }
    }
}
