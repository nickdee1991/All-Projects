using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    private Animator anim;
    public LayerMask viewMask;
    private PatrolRandom patrolRandom;
    private AudioManager aud;

    public Transform playerPosition;
    public Transform[] spawnPoints;
    private int spawnPosition;

    public Light spotlight;

    Color originalSpotlightColour;

    public float viewDistance = 15;
    public float viewAngle = 150;
    public int patrolSpeed = 2;
    private int lateStart = 2;

    private float playerVisibleTimer;
    public float timeToSpotPlayer = 0.5f;

    public bool detectedEnemy;

    private void Start()
    {
        anim = GetComponent<Animator>();
        patrolRandom = GetComponent<PatrolRandom>();
        aud = FindObjectOfType<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;

        StartCoroutine(LateStart(lateStart));

        viewAngle = spotlight.spotAngle;
        originalSpotlightColour = spotlight.color;
    }

    //spawn enemy only after room generation has finished
    IEnumerator LateStart(float waitTime)
    {
        spawnPosition = Random.Range(0, spawnPoints.Length);
        yield return new WaitForSeconds(waitTime);
        transform.parent.position = spawnPoints[spawnPosition].position;
        transform.parent.rotation = spawnPoints[spawnPosition].rotation;
        transform.position = spawnPoints[spawnPosition].position;
        //transform.rotation = spawnPosition.rotation;
    }

private void Update()
    {
        if (CanSeePlayer())
        {
            playerVisibleTimer += Time.deltaTime;
        }else{
            detectedEnemy = false;
            patrolRandom.enabled = true;
            playerVisibleTimer -= Time.deltaTime;
            if (patrolRandom == null)
            {
                patrolRandom = GetComponent<PatrolRandom>();
                if (patrolRandom.isActiveAndEnabled)
                {
                    patrolRandom.agent.speed = 2.5f;
                }
                else { return; }
            }

        }
        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotlight.color = Color.Lerp(originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer)
        {
            detectedEnemy = true;
            Attack();
        }else{
            detectedEnemy = false;
            patrolRandom.enabled = true;
            anim.SetBool("isRunning", false);
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
        StopAllCoroutines();
        patrolRandom.enabled = false;
        //move towards player
        MoveToTarget();
    }


    public void MoveToTarget()
    {
        if (player.GetComponent<PlayerCaught>().Captured == false)
        {
            if (patrolRandom.enabled == false)
            {
                patrolRandom.enabled = true;
            }
            anim.SetBool("isRunning", true);
            patrolRandom.agent.destination = player.transform.position;
            patrolRandom.agent.speed = 6;
        }
    }
}
