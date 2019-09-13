using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour{

    //private GameObject player;
    public GameObject elevator;
    public Animator animatorInner;
    public bool ElevatorKeyActivated;
    private GameObject player;

    public bool ElevatorDescended;

    // Use this for initialization
    void Start()
    {
        ElevatorDescended = false;
        ElevatorKeyActivated = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ElevatorKeyActivated == true)
            {
                Debug.Log("Terminal Button pressed");
                //Physics.IgnoreCollision(player.GetComponent<SphereCollider>(), this.GetComponent<BoxCollider>());
                animatorInner.SetBool("ElevatorInnerOpen", true);

                if (elevator.GetComponent<ElevatorEnteredCheck>().ElevatorEntered == true && ElevatorDescended == false)
                {
                    Debug.Log("In Elevator, ready to descend and " + "ElevatorKey " + ElevatorKeyActivated);
                    //animatorInner.SetBool("ElevatorInnerOpen", true);
                    animatorInner.SetBool("ElevatorAscend", false);
                    animatorInner.SetBool("ElevatorDescend", true);

                    if (animatorInner.GetCurrentAnimatorStateInfo(0).IsName("ElevatorDescend") &&
                        animatorInner.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        Debug.Log("ElevatorDescended " + ElevatorDescended);
                        ElevatorDescended = true;
                    }
                }

                if (elevator.GetComponent<ElevatorEnteredCheck>().ElevatorEntered == true && ElevatorDescended == true)
                {
                    Debug.Log("ready to ascend");
                    //animatorInner.SetBool("ElevatorInnerOpen", true);
                    animatorInner.SetBool("ElevatorDescend", false);
                    animatorInner.SetBool("ElevatorAscend", true);


                    if (animatorInner.GetCurrentAnimatorStateInfo(0).IsName("ElevatorAscend") &&
                        animatorInner.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        Debug.Log("ElevatorDescended " + ElevatorDescended);
                        ElevatorDescended = false;
                    }
                }
                if (elevator.GetComponent<ElevatorEnteredCheck>().ElevatorEntered == false)
                {
                    //animatorInner.SetBool("ElevatorInnerOpen", false);
                    animatorInner.SetBool("ElevatorAscend", false);
                    animatorInner.SetBool("ElevatorDescend", false);
                    ElevatorDescended = false;
                }
            }
        }
    }
}
