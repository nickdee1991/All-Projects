using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorKey : MonoBehaviour {

    public GameObject ElevatorOuter;
    public GameObject ElevatorInner;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("ElevatorKey activated");
            ElevatorOuter.GetComponent<ElevatorController>().ElevatorKeyActivated = true;
            ElevatorInner.GetComponent<ElevatorController>().ElevatorKeyActivated = true;
        }
    }
}
