using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetDoors : MonoBehaviour
{
    public bool doorOpened;

    private void Start()
    {
        doorOpened = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E)&& doorOpened == false)
        {
            doorOpened = true;
            GetComponent<Animator>().SetBool("open", true);
        }
        else if (Input.GetKeyDown(KeyCode.E) && doorOpened)
        {
            doorOpened = false;
            GetComponent<Animator>().SetBool("open", false);
        }
    }
}
