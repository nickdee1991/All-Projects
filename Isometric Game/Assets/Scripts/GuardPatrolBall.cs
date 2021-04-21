using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GuardPatrolBall : MonoBehaviour {

    public float patrolSpeed = 3;
    public float waypointWaitTime = 0.5f;
    public float turnSpeed = 90;
    public float waitTime = .75f;
    public float stopDistance = 1;
    public float wheelRotation;
    public GameObject wheel;

    private int destPoint = 0;

    public float playerVisibleTimer;

    public float timeToSpotPlayer;

    private float currentTime;

    public bool isMoving;
    public bool detectedPlayer;

    private Animator anim;
    private NavMeshAgent nav;
    private LevelManager levelManager;

    public Transform[] patrolPoints;



    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInParent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        //nav.autoBraking = false;

        GoToNextPoint();
    }

    private void Update()
    {
        wheel.transform.Rotate(new Vector3(Time.deltaTime * -wheelRotation,0 , 0 ));

        if (detectedPlayer)
        {
            playerVisibleTimer += Time.deltaTime;
        }
        else
        {
            playerVisibleTimer -= Time.deltaTime;
        }

        if (playerVisibleTimer < 0)
        {
            playerVisibleTimer = 0;
        }
        if (playerVisibleTimer > timeToSpotPlayer)
        {
            detectedPlayer = true;
            playerVisibleTimer = timeToSpotPlayer;
            Debug.Log("player detected");
        }

        //playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);

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
            anim.SetBool("attack", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerVisibleTimer -= Time.deltaTime;
            detectedPlayer = false;
            anim.SetBool("attack", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) //&& playerVisibleTimer >= timeToSpotPlayer && detectedPlayer == false
        {
            playerVisibleTimer += Time.deltaTime;
            detectedPlayer = true;
            levelManager.GameOver();
        }
        else
        {
            detectedPlayer = false;
        }
    }
}
