using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour {

    public float patrolSpeed = 3;
    public float waypointWaitTime = 0.5f;
    public float turnSpeed = 90;
    private GameObject player;
    public LayerMask viewMask;

    public Light spotlight;

    public float viewDistance;
    float viewAngle;

    public Transform pathHolder;
    Transform playerPosition;
    Color originalSpotlightColour;

    public float health;
    public float pointsToGive;
    public float sightDistance;
    public float waitTime = 1;
    public float stopDistance;

    private float currentTime;
    private bool shot;

    public bool detectedEnemy;

    public Transform deathParticleSpawn;
    public Transform bulletSpawnPoint;
    private Transform bulletSpawned;
    private Transform pistolHolder;

    public GameObject deathParticle;
    public GameObject bullet;

    public Animator animator;

    public LineRenderer enemySight;
    public Gradient redColor;
    public Gradient greenColor;
    public Gradient yellowColor;

    public RaycastHit hitInfo;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotlight.spotAngle;
        originalSpotlightColour = spotlight.color;

        pistolHolder = this.transform.GetChild(1);
        bulletSpawnPoint = pistolHolder.GetChild(1);

        //converts the waypoints into positions the Guard can move to.
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
        }
        StartCoroutine(FollowPath(waypoints));
    }

    private void Update()
    {
        if (CanSeePlayer ())
        {
            spotlight.color = Color.yellow;
        } else {
            spotlight.color = originalSpotlightColour;
        }


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
        print(this.gameObject.name + " has died!");
        deathParticleSpawn = Instantiate(deathParticle.transform, deathParticleSpawn.transform.position, Quaternion.identity);
        Destroy(this.gameObject, 1f);
        player.GetComponent<Player>().points += pointsToGive;

    }

    //target player and fire
    public void Attack()
    {
        detectedEnemy = true;
        //red ray
        Debug.DrawRay(transform.position, hitInfo.point, Color.red);

        //enemySight.colorGradient = redColor;

        //look at player
        transform.LookAt(player.transform.position);

        //move towards player
        MoveToTarget();

        #region shoot at player 

        if (currentTime == 0)
        {
            Shoot();
        }

        #endregion

        //if enemy moves too close, stop at a distance until player leaves space
        if (Vector3.Distance(transform.position, player.transform.position) < stopDistance)
        {
            patrolSpeed = 0;
        }
        else
        {
            patrolSpeed = 3;
            MoveToTarget();
        }
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

    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, playerPosition.position) < viewDistance)
        {
            Vector3 dirToPlayer = (playerPosition.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, playerPosition.position, viewMask))
                {
                    Attack();
                    MoveToTarget();
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


    public void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPosition.transform.position, patrolSpeed * 2 * Time.deltaTime);
    }

    IEnumerator FollowPath (Vector3[] waypoints)
    {
        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, patrolSpeed * Time.deltaTime);
            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waypointWaitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));
            }
            yield return null;
        }
    }

    IEnumerator TurnToFace (Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    //will draw a visual aid to see where Guard is moving
    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .5f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}
