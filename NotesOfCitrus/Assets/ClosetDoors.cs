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
            GetComponent<Animator>().SetBool("open", true);
            doorOpened = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && doorOpened)
        {
            GetComponent<Animator>().SetBool("open", false);
            doorOpened = false;
        }
    }
}
