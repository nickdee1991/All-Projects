using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalPatrol : MonoBehaviour {

    //public static event System.Action OnGuardHasSpottedPlayer;

    public float patrolSpeed = 3;
    public float waypointWaitTime = 0.5f;
    public float turnSpeed = 90;
    float viewAngle;

    public Transform pathHolder;
    Transform playerPosition;

    public float sightDistance;
    public float waitTime = 1;
    public float stopDistance;

    private float currentTime;

    public Animator animator;

    public RaycastHit hitInfo;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //converts the waypoints into positions the Guard can move to.
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
        }

        StartCoroutine(FollowPath(waypoints));
    }

    IEnumerator FollowPath (Vector3[] waypoints)
    {
        animator.SetBool("isMoving", true);
        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, patrolSpeed * Time.deltaTime);
            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (Random.Range(-1 , waypoints.Length));
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
    }
}
