using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAggro : MonoBehaviour
{
    private GameObject player;
    private GameObject eye;
    private LevelDirector levelDirector;
    private NavMeshAgent agent;
    public InteractableManager IntMgr;
    public Animator anim;
    public AudioManager audioMgr;

    public Transform playerPosition;

    public bool FoundPlayer;

    public LayerMask viewMask;

    public Light spotlight;

    Color originalSpotlightColour;

    public float viewDistance;
    public float viewAngle;
    public float patrolSpeed = 3;
    public float waypointWaitTime = 0.5f;
    public float sightDistance;
    public float waitTime = .75f;
    public float timeToSpotPlayer = .5f;

    private float playerVisibleTimer;

    void Start()
    {
        FoundPlayer = false;
        eye = GameObject.Find("ClaudiaEye");
        eye.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;
        originalSpotlightColour = spotlight.color;
        levelDirector = FindObjectOfType<LevelDirector>();
        IntMgr = FindObjectOfType<InteractableManager>();
        audioMgr = FindObjectOfType<AudioManager>();
        anim = GetComponentInChildren<Animator>();

    }
    private void Update()
    {


        if (CanSeePlayer())
        {
            playerVisibleTimer += Time.deltaTime;
        }else{
            playerVisibleTimer -= Time.deltaTime;
        }

        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotlight.color = Color.Lerp(originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer && !FoundPlayer)
        {
            Aggro();
            Debug.Log("Attacking Player");
        }else{
            //Debug.Log("Lost Sight of player");
            FoundPlayer = false;
        }
    }

    private void Aggro()
    {
        FoundPlayer = true;
        Debug.Log("Player Spotted");
        agent.destination = player.transform.position;
        audioMgr.PlaySound("Aggro");
        eye.SetActive(true);
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

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && IntMgr.itemCollected)
        {
            agent.isStopped = true;
            agent.speed = 0;
            anim.SetBool("enemyDied", true);
            Debug.Log("Claudia dead");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(player))
        {
            levelDirector.GameOver = true;
            levelDirector.PlayerDead();
            levelDirector.CursorUnlocked();
            Debug.Log("Player dead");
        }
    }
}
