using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolRandom: MonoBehaviour
{
    public Transform[] patrolPoints;
    private int destPoint = 0;
    public NavMeshAgent agent;
    private Animator anim;

    private int lateStart = 2;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        agent.autoBraking = false;

        anim = GetComponent<Animator>();

        StartCoroutine(LateStart(lateStart));
    }

    //spawn enemy only after room generation has finished
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
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
        anim.SetBool("isMoving",true);
        //Debug.Log(destPoint);

        //choose next point in array
        //moving to start if needed
        destPoint = (destPoint + 1) % patrolPoints.Length;


    }
}
