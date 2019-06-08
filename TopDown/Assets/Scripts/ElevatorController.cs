using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour{

    //private GameObject player;
    public GameObject elevator;
    public Animator animatorOuter;
    public Animator animatorInner;
    public bool ElevatorKeyActivated;

    private Vector3 startPos;
    private Vector3 endPos;

    private bool doorOpen = false;
    private bool doorPassive = true;

    private float distance = 10f;
    private float lerpTime = .5f;
    private float currentLerpTime = 0;

    // Use this for initialization
    void Start()
    {
        ElevatorKeyActivated = false;
        //player = GameObject.FindGameObjectWithTag("Player");
        startPos = transform.position;
        endPos = transform.position + Vector3.down * distance;
    }

    private void Update()
    {
        if (doorOpen == true)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float Perc = currentLerpTime / lerpTime;
            elevator.transform.position = Vector3.Lerp(startPos, endPos, Perc);
            doorPassive = false;
        }
        if (doorOpen == false && doorPassive == false)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float Perc = currentLerpTime / lerpTime;
            elevator.transform.position = Vector3.Lerp(endPos, startPos, Perc);
        }
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
                animatorOuter.SetBool("ElevatorOuterOpen", true);
                animatorInner.SetBool("ElevatorInnerOpen", true);
                animatorInner.SetBool("ElevatorDescend", true);
                //doorOpen = true;
                //ElevatorDescend();
            }
        }
    }

    private void ElevatorDescend()
    {
        Debug.Log("elevator going down");
        currentLerpTime = 0;
        float Perc = currentLerpTime / lerpTime;
        elevator.transform.position = Vector3.Lerp(startPos, endPos, Perc);
    }

    private void ElevatorAscend()
    {
        currentLerpTime = 0;
        Debug.Log("Door Closing");
        float Perc = currentLerpTime / lerpTime;
        elevator.transform.position = Vector3.Lerp(endPos, startPos, Perc);
    }
}
