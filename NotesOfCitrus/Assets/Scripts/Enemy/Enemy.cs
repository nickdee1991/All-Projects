using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager gm;  
    public GameObject player;
    public GameObject InvestigateLocation;

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
    public float patrolSpeed;
    public float attackSpeed;
    private int lateStart = 2;

    private float playerVisibleTimer;
    public float timeToSpotPlayer = 0.5f;

    public bool detectedEnemy;

    private void Start()
    {

        anim = GetComponent<Animator>();
        patrolRandom = GetComponent<PatrolRandom>();
        gm = FindObjectOfType<GameManager>();
        aud = FindObjectOfType<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;

        //StartCoroutine(LateStart(lateStart)); //disable this for now

        viewAngle = spotlight.spotAngle;
        originalSpotlightColour = spotlight.color;

        if (gm.DEBUGMODE)
        {
            spotlight.intensity = 4;
        }
    }

    //spawn enemy only after room generation has finished
    IEnumerator LateStart(float waitTime)
    {
        spawnPosition = Random.Range(0, spawnPoints.Length);
        yield return new WaitForSeconds(waitTime);

        Debug.Log("Late Start");
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
            patrolRandom.agent.speed = patrolSpeed; //Commented this to stop beartrap trigger from reverting speed
            if (patrolRandom == null) // check for patrolRandom script
            {
                patrolRandom = GetComponent<PatrolRandom>();
                if (patrolRandom.isActiveAndEnabled)
                {
                    patrolRandom.agent.speed = patrolSpeed;
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
                if (!Physics.Linecast(transform.position, playerPosition.position, viewMask) && !gm.DEBUGMODE)
                {
                    Debug.Log(gameObject.name + " can see player");
                    Attack();
                    return true;
                }
            }
        }
        return false;

    }

    public void Investigate()
    {
        Transform InvestigateLocationPosition; // create a transform of the position of noise/evidence etc
        InvestigateLocationPosition = InvestigateLocation.transform; // any noise that enemy is within radius of will become "InvestigateLocation"
        patrolRandom.agent.destination = InvestigateLocationPosition.transform.position; // next new patrol point becomes "InvestigateLocationPosition"
    }

    public void Attack()
    {
        if (player.GetComponent<PlayerCaught>().Captured == false)
        {
            StopAllCoroutines();
            detectedEnemy = true;
            patrolRandom.enabled = false; // stop patrolling
            patrolRandom.agent.speed = attackSpeed; // start running
            anim.SetBool("isRunning", true); // start animation
            patrolRandom.agent.destination = player.transform.position; // set next waypoint to player
        }
    }

    public void LostSight()
    {
        //store last known player position
        //move there and wait a set amount
        //look around a bit
        //resume patrol
    }
}
