using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public LayerMask viewMask;

    public Transform playerPosition;
    public Transform spawnPosition;

    public Light spotlight;

    Color originalSpotlightColour;

    public float viewDistance = 15;
    public float viewAngle = 150;
    public int patrolSpeed = 2;
    private int lateStart = 1;

    private float playerVisibleTimer;
    public float timeToSpotPlayer = 0.5f;

    public bool detectedEnemy;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;

        StartCoroutine(LateStart(lateStart));

        viewAngle = spotlight.spotAngle;
        originalSpotlightColour = spotlight.color;
    }

    //spawn enemy only after room generation has finished
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        transform.parent.position = spawnPosition.position;
        transform.parent.rotation = spawnPosition.rotation;
        transform.position = spawnPosition.position;
        //transform.rotation = spawnPosition.rotation;
    }

private void Update()
    {
        if (CanSeePlayer())
        {
            playerVisibleTimer += Time.deltaTime;
        }else{
            playerVisibleTimer -= Time.deltaTime;
            if (GetComponent<PatrolRandom>().isActiveAndEnabled)
            {
                GetComponent<PatrolRandom>().agent.speed = 2.5f;
            }
            else { return; }
        }
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotlight.color = Color.Lerp(originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer)
        {
            detectedEnemy = true;
            Attack();
        }
        else
        {
            detectedEnemy = false;
        }
    }

    public bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, playerPosition.position) < viewDistance)
        {
            Vector3 dirToPlayer = (playerPosition.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(-transform.right, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, playerPosition.position, viewMask))
                {
                    Debug.Log(gameObject.name + " can see player");
                    Attack();
                    return true;
                }
            }
        }
        return false;
    }

    public void Attack()
    {
        detectedEnemy = true;

        //move towards player
        MoveToTarget();
    }


    public void MoveToTarget()
    {
        if (player.GetComponent<PlayerCaught>().Captured == false)
        {
            GetComponent<PatrolRandom>().agent.destination = player.transform.position;
            GetComponent<PatrolRandom>().agent.speed = 5;
        }
    }
}
