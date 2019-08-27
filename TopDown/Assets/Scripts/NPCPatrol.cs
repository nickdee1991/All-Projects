using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour {

    public float patrolSpeed = 3;
    public float waypointWaitTime = 0.5f;
    public float turnSpeed = 90;
    private GameObject player;
    public LayerMask viewMask;

    public float health = 3;
    public float sightDistance;
    public float waitTime = 1;
    public float stopDistance = 1;
    public float timeToSpotPlayer = 2f;

    private float playerVisibleTimer;
    private float wheelRotation = 300;
    private float currentTime;

    private int destPoint = 0;
    public bool isMoving;

    private NavMeshAgent nav;

    public Transform playerPosition;
    public Transform[] patrolPoints;
    public Transform deathParticleSpawn;

    public GameObject deathParticle;
    public GameObject wheel;

    public Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;

        nav = GetComponent<NavMeshAgent>();
        //nav.autoBraking = false;

        GoToNextPoint();
    }

    private void Update()
    {
        if (isMoving == true)
        {
            wheel.transform.Rotate(new Vector3(Time.deltaTime * wheelRotation, 0, 0));
        }

        //choose the next destination point when the agent gets
        //close to the current one.
        if (!nav.pathPending && nav.remainingDistance < stopDistance)       
            GoToNextPoint();        


        //if the health of this object reaches X then destroy and spawn particle effect
        if (health <= 0)
        {
            Die();
        }
    }

    //instantiate particle effect and destroy this object
    public void Die()
    {
        //destroy object, spawn particle effect, give point to player
        print(gameObject.name + " has died!");
        deathParticleSpawn = Instantiate(deathParticle.transform, deathParticleSpawn.transform.position, Quaternion.identity);
        Destroy(gameObject, 1f);

    }

    // if enemy is shot look at player and fire back
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Enemy: I've been shot!");
        }
    }

    void GoToNextPoint()
    {
        //move robt wheels
        isMoving = true;

        //Returns if no points have been set up
        if (patrolPoints.Length == 0) 
            return;
        
        //set the agent to go to the currently selected destination.
        nav.destination = patrolPoints[destPoint].position;

        //choose the next point in the array as the destination
        //cycling to the start if neccesary
        destPoint = (destPoint + 1) % patrolPoints.Length;
    }
}
