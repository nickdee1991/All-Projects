using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrol : MonoBehaviour {

    private GameObject player;
    public LayerMask viewMask;

    public Light spotlight;

    Color originalSpotlightColour;

    public float viewDistance;
    public float viewAngle;
    public float patrolSpeed = 3;
    public float waypointWaitTime = 0.5f;
    public float turnSpeed = 90;
    public float stunnedTime = 5f;
    public float health = 3;
    public float sightDistance;
    public float waitTime = 1;
    public float stopDistance = 1;
    public float timeToSpotPlayer = .5f;

    private float playerVisibleTimer;
    private float wheelRotation = 150;
    private float currentTime;

    private int destPoint = 0;

    public bool detectedEnemy;
    public bool isMoving;
    public bool DisablePlayer = false;
    private bool shot;


    private NavMeshAgent nav;

    public Transform[] patrolPoints;
    public Transform playerPosition;
    public Transform deathParticleSpawn;
    public Transform bulletSpawnPoint;
    private Transform bulletSpawned;
    private Transform pistolHolder;

    public GameObject deathParticle;
    public GameObject bullet;
    public GameObject wheel1;
    public GameObject wheel2;

    public Animator animator;

    public RaycastHit hitInfo;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;
        viewAngle = spotlight.spotAngle;
        originalSpotlightColour = spotlight.color;

        pistolHolder = transform.GetChild(1);
        bulletSpawnPoint = pistolHolder.GetChild(1);
        nav = GetComponent<NavMeshAgent>();
        //nav.autoBraking = false;

        GoToNextPoint();
        PlayerLastPos();
    }

    private void Update()
    {
        if (isMoving == true)
        {
            wheel1.transform.Rotate(new Vector3(0, 0, Time.deltaTime * wheelRotation));
            wheel2.transform.Rotate(new Vector3(0, 0, Time.deltaTime * wheelRotation));
        }

        if (CanSeePlayer())
        {
            playerVisibleTimer += Time.deltaTime;
        }else{
            playerVisibleTimer -= Time.deltaTime;
        }
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotlight.color = Color.Lerp(originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer)
        {
            detectedEnemy = true;
            Attack();
            Debug.Log("Attacking Player");
        } else {
            //Debug.Log("Lost Sight of player");
            detectedEnemy = false;           
        }

        //choose the next destination point when the agent gets
        //close to the current one.
        if (!nav.pathPending && nav.remainingDistance < stopDistance)       
            GoToNextPoint();        

        //counting down when to shoot
        if (shot && currentTime < waitTime)
        {
            currentTime += 1 * Time.deltaTime;
        }
        if (currentTime >= waitTime)
        {
            currentTime = 0;
        }

        //if there is no bullet spawn point for Enemy - assign one within object
        if (!bulletSpawnPoint)
        {
            pistolHolder = this.transform.GetChild(0);
            bulletSpawnPoint = pistolHolder.GetChild(1);
        }

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
        //Destroy(gameObject, 1f);

        StartCoroutine(isStunned());

    }

    IEnumerator isStunned()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<GuardPatrol>().isMoving = false;
        gameObject.GetComponent<GuardPatrol>().timeToSpotPlayer = stunnedTime;
        gameObject.GetComponentInChildren<Light>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().speed = 0;
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<GuardPatrol>().isMoving = true;
        gameObject.GetComponent<GuardPatrol>().timeToSpotPlayer = timeToSpotPlayer;
        gameObject.GetComponentInChildren<Light>().enabled = true;
        gameObject.GetComponent<NavMeshAgent>().speed = 3;
        animator.SetBool("isMoving", true);
        StopCoroutine(isStunned());
    }

    //target player and fire
    public void Attack()
    {
        isMoving = true;
            //red ray
            //Debug.DrawRay(transform.position, hitInfo.point, Color.red);

            //look at player
            nav.SetDestination(playerPosition.localPosition);

        //trying to auto kill player once enemy reaches them
        if (DisablePlayer == true)
        {
            player.GetComponent<Player>().health = 0;
        }
        //if (nav.Raycast(this.gameObject.transform, player.GetComponent<Collider>() ))


            #region shoot at player 

            if (currentTime == 0)
            {
                Shoot();
            }

            #endregion
    }

    // if enemy is shot look at player and fire back
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Enemy: I've been shot!");
            Attack();
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

    IEnumerator PlayerLastPos()
    {
        return null;
    }

    public bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, playerPosition.position) < viewDistance)
        {
            Vector3 dirToPlayer = (playerPosition.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, playerPosition.position, viewMask))
                {
                    //Debug.Log("Can See Player. Attacking.."); - Yes the enemy is constantly updating to check that it can see player.
                    //need to store this and reference it once enemy loses sight
                    //Attack();
                    Vector3 lastKnownPos = (playerPosition.position);
                    Debug.Log(lastKnownPos);
                    return true;
                }
            }
        }
        return false;
    }

    public void Shoot()
    {
        {
            shot = true;
            bulletSpawned = Instantiate(bullet.transform, bulletSpawnPoint.position, Quaternion.identity);
            bulletSpawned.rotation = this.transform.rotation;
        }
    }

    #region TurnToFace (deprecated)

    IEnumerator TurnToFace (Vector3 lookTarget)
    {
        animator.SetBool("isMoving", false);
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            isMoving = false;
            float angle = Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("colliding with player");
            DisablePlayer = true;
        }
    }
}
