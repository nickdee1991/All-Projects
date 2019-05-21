using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionDoorKey : MonoBehaviour {

    public bool fusePickedUp = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            fusePickedUp = true;
            Debug.Log("Reception Door Fuse Picked Up");
        }
    }
}
