using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour{

    //private GameObject player;
    public GameObject elevator;
    public Animator animatorOuter;
    public Animator animatorInner;
    public bool ElevatorKeyActivated;
    private GameObject player;

    private bool ElevatorDescended = false;

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
            Physics.IgnoreCollision(player.GetComponent<SphereCollider>(), this.GetComponent<BoxCollider>());
            animatorInner.SetBool("ElevatorInnerOpen", true);

            if (elevator.GetComponent<ElevatorEnteredCheck>().ElevatorEntered == true)
            {
                Debug.Log("In Elevator, ready to descend and " + "ElevatorKey " + ElevatorKeyActivated);
                animatorInner.SetBool("ElevatorInnerOpen", true);
                animatorInner.SetBool("ElevatorAscend", false);
                animatorInner.SetBool("ElevatorDescend", true);
            }else

            if (elevator.GetComponent<ElevatorEnteredCheck>().ElevatorEntered == true)
            {
                Debug.Log("ready to ascend");
                animatorInner.SetBool("ElevatorDescend", false);
                animatorInner.SetBool("ElevatorAscend", true);
                animatorInner.SetBool("ElevatorInnerOpen", true);
                //ElevatorDescended = false;

            }
            if (elevator.GetComponent<ElevatorEnteredCheck>().ElevatorEntered == false)
            {
                animatorInner.SetBool("ElevatorInnerOpen", false);
                animatorInner.SetBool("ElevatorAscend", false);
                animatorInner.SetBool("ElevatorDescend", false);
            }
        }
    }
}
