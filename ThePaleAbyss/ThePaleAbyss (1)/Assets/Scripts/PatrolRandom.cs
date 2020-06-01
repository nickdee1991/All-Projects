using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolRandom : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int destPoint = 0;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        //choose next patrolPoint when the agent gets close to current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }

    void GoToNextPoint()
    {
        //returns null if no points
        if (patrolPoints.Length == 0)
        {
            return;
        }


        //send agent to current point
        agent.destination = patrolPoints[destPoint].position;

        Debug.Log(destPoint);

        //choose next point in array
        //moving to start if needed
        destPoint = (destPoint + 1) % patrolPoints.Length;


    }
}
