using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GuardPatrolButton : MonoBehaviour {

    public float patrolSpeed = 3;
    public float waypointWaitTime = 0.5f;
    public float turnSpeed = 90;
    public float waitTime = .75f;
    public float stopDistance = 1;

    private int destPoint = 0;

    public GameObject door;

    private Animator anim;
    private NavMeshAgent nav;
    private AudioManager aud;

    public Transform[] patrolPoints;



    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInParent<Animator>();
        aud = FindObjectOfType<AudioManager>();
        //nav.autoBraking = false;

        GoToNextPoint();
    }

    private void Update()
    {
        //choose the next destination point when the agent gets
        //close to the current one.
        if (!nav.pathPending && nav.remainingDistance < stopDistance)       
            GoToNextPoint();
    }

    void GoToNextPoint()
    {

        //Returns if no points have been set up
        if (patrolPoints.Length == 0) 
            return;
        
        //set the agent to go to the currently selected destination.
        nav.destination = patrolPoints[destPoint].position;

        //choose the next point in the array as the destination
        //cycling to the start if neccesary
        destPoint = (destPoint + 1) % patrolPoints.Length;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            aud.PlaySound("dooropen");
            anim.SetBool("pressed", true);
            door.GetComponent<Animator>().SetBool("Open", true);
            nav.speed = 0;
        }
    }
}
