using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{

    private GameObject player;
    public GameObject elevator;
    public Animator animatorOuter;
    public Animator animatorInner;
    public bool ElevatorKeyActivated;

    // Use this for initialization
    void Start()
    {
        ElevatorKeyActivated = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseDown()
    {
        if (ElevatorKeyActivated == true)
        {
            animatorOuter.SetBool("ElevatorOuterOpen", true);
            animatorInner.SetBool("ElevatorInnerOpen", true);

            if (elevator.GetComponent<ElevatorEnteredCheck>().ElevatorEntered == true)
            {
                Debug.Log("In Elevator, ready to descend and " + "ElevatorKey " + ElevatorKeyActivated);
                animatorOuter.SetBool("ElevatorOuterOpen", false);
                animatorInner.SetBool("ElevatorInnerOpen", true);
                animatorInner.SetBool("ElevatorDescend", true);
            }
        }
    }
}
