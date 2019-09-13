using UnityEngine;



public class Boss_AI : MonoBehaviour {

    private GameObject player;
    public GameObject guard;
    public GameObject[] guardGroup;
    public GameObject guardShell;
    

    public float rotation = 50;
    public float sightDistance;
    public float timeToSpotPlayer = .5f;
    public float viewDistance;
    public float viewAngle;

    private float playerVisibleTimer;
    private float currentTime;

    public bool detectedEnemy;

    public Transform playerPosition;
    public LayerMask viewMask;

    public Light spotlight;

    Color originalSpotlightColour;

    public void Awake()
    {
        guard = GameObject.FindGameObjectWithTag("Enemy");
        //guardGroup = GameObject.FindGameObjectsWithTag("");
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(guard);
    }

    void Start()
    {
        playerPosition = player.transform;
        viewAngle = spotlight.spotAngle;
        originalSpotlightColour = spotlight.color;
    }

	public void Update () {
        transform.Rotate (new Vector3(0, Time.deltaTime * rotation, 0 ));

        if (CanSeePlayer())
        {
            playerVisibleTimer += Time.deltaTime;
        }else{
            playerVisibleTimer -= Time.deltaTime;
        }

        playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
        spotlight.color = Color.Lerp(originalSpotlightColour, Color.blue, playerVisibleTimer / timeToSpotPlayer);

        if (playerVisibleTimer >= timeToSpotPlayer)
        {
            guardShell.BroadcastMessage("Attack");
            Debug.Log("Can See player, Alerting Guards");
            detectedEnemy = true;
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
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, playerPosition.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}
