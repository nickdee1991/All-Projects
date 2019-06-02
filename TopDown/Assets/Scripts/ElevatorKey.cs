using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorKey : MonoBehaviour {

    public GameObject ElevatorOuter;
    public GameObject ElevatorInner;
    public GameObject floatingText;

    // Use this for initialization
    private void OnMouseEnter()
    {
        floatingText.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        floatingText.gameObject.SetActive(false);
    }


    private void OnMouseDown()
    {
        Debug.Log("ElevatorKey activated");
        ElevatorOuter.GetComponent<ElevatorController>().ElevatorKeyActivated = true;
        ElevatorInner.GetComponent<ElevatorController>().ElevatorKeyActivated = true;
        gameObject.SetActive(false);
    }
}
