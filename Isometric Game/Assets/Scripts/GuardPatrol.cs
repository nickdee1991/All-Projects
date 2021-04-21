using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GuardPatrol : MonoBehaviour {

    private GameObject playerBox;
    private GameObject playerTri;
    private GameObject playerWheel;
    public TMPro.TextMeshProUGUI alertText;
    public LayerMask viewMask;

    public Light spotlight;
    public LevelManager levelManager;
    Color originalSpotlightColour;

    public float viewDistance;
    public float viewAngle;
    public float patrolSpeed = 3;
    public float waypointWaitTime = 0.5f;
    public float turnSpeed = 90;
    public float sightDistance;
    public float waitTime = .75f;
    public float stopDistance = 1;
    public float timeToSpotPlayer = .5f;

    public float playerVisibleTimer;
    private float currentTime;

    private int destPoint = 0;

    public bool detectedPlayer;
    public bool isMoving;


    private NavMeshAgent nav;

    public Transform[] patrolPoints;
    public Transform playerPosition;


    private void Start()
    {
        playerBox = GameObject.Find("PlayerBox");
        playerTri = GameObject.Find("PlayerTri");
        playerWheel = GameObject.Find("PlayerWheel");
        viewAngle = spotlight.spotAngle;
        originalSpotlightColour = spotlight.color;
        detectedPlayer = false;
        nav = GetComponent<NavMeshAgent>();
        levelManager = FindObjectOfType<LevelManager>();
        //nav.autoBraking = false;

        GoToNextPoint();
    }

    private void Update()
    {

        if (CanSeePlayer())
        {
            playerVisibleTimer += Time.deltaTime;
            alertText.enabled = true;
        }else{
            playerVisibleTimer -= Time.deltaTime;
            alertText.enabled = false;
        }
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotlight.color = Color.Lerp(originalSpotlightColour, Color.yellow, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer && detectedPlayer == false)
        {
            levelManager.gameOver = true;
            levelManager.GaemOver();
            detectedPlayer = true;
            Debug.Log("Player Detected");
        }

        //choose the next destination point when the agent gets
        //close to the current one.
        if (!nav.pathPending && nav.remainingDistance < stopDistance)       
            GoToNextPoint();        

        if (currentTime >= waitTime)
        {
            currentTime = 0;
        }

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

    public bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, playerBox.transform.position) < viewDistance || Vector3.Distance(transform.position, playerTri.transform.position) < viewDistance || Vector3.Distance(transform.position, playerWheel.transform.position) < viewDistance) // if player is within the distance of enemy spotlight
        {
            Vector3 dirToPlayerBox = (playerBox.transform.position - transform.position).normalized;
            Vector3 dirToPlayerTri = (playerTri.transform.position - transform.position).normalized;
            Vector3 dirToPlayerWheel = (playerWheel.transform.position - transform.position).normalized;
            float angleBetweenGuardAndPlayerBox = Vector3.Angle(transform.forward, dirToPlayerBox);
            float angleBetweenGuardAndPlayerTri = Vector3.Angle(transform.forward, dirToPlayerTri);
            float angleBetweenGuardAndPlayerWheel = Vector3.Angle(transform.forward, dirToPlayerWheel);
            if (angleBetweenGuardAndPlayerBox < viewAngle / 2f || angleBetweenGuardAndPlayerTri < viewAngle / 2f || angleBetweenGuardAndPlayerWheel < viewAngle / 2f) // if player is within the spotlight cone
            {
                if (!Physics.Linecast(transform.position, playerBox.transform.position, viewMask) || !Physics.Linecast(transform.position, playerTri.transform.position, viewMask) || !Physics.Linecast(transform.position, playerWheel.transform.position, viewMask)) // if player is within distance and cone of spotlight and no obstacles are in way
                {
                    return true;
                }
            }
        }
        return false;

    }
}
