using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolRandom: MonoBehaviour
{
    //list for patrol points assigned to this agent
    public Transform[] patrolPoints;
    //the current destination
    private int destPoint = 0;
    public NavMeshAgent agent;
    private Enemy enemy;
    private Animator anim;
    private bool isWaiting;

    private float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        agent = GetComponentInParent<NavMeshAgent>();
        agent.autoBraking = false;

        anim = GetComponent<Animator>();

        GoToNextPoint();
    }

    void Update()
    {
        //choose next patrolPoint when the agent gets close to current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f && isWaiting == false && enemy.detectedEnemy == false)
        {
            isWaiting = true;
            agent.isStopped = true;
            anim.SetBool("isMoving", false);
            StartCoroutine(WaitAtPatrolPoint());
        }

        if (!agent)
        {
            agent = GetComponentInParent<NavMeshAgent>();
        }

        if (enemy.detectedEnemy)
        {
            waitTime = 0;
            enemy.Attack();
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
        anim.SetBool("isMoving",true);
        //Debug.Log(destPoint);

        //choose next point in array
        //moving to start if needed
        destPoint = (destPoint + 1) % patrolPoints.Length;
    }

    IEnumerator WaitAtPatrolPoint()
    {
        waitTime = Random.Range(1, 3);
        //Debug.Log(waitTime);
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        agent.isStopped = false;
        GoToNextPoint();
    }
}
