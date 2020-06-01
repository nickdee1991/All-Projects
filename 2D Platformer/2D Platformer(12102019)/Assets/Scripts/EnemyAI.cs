using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{

    public CircleCollider2D spotDistance;
    public Animator Anim;

    // What to chase? brackeys muh ni$$a
    public Transform target;
    public Transform enemyGraphics;

    private SceneDirector sceneDirector;

    // How many times each second we will update our path
    public float updateRate = 2f;
    private float prevPos;

    // Caching
    private Seeker seeker;
    private Rigidbody2D rb;

    //The calculated path
    public Path path;

    //The AI's speed per second
    public float speed = 300f;
    public ForceMode2D fMode;

    [HideInInspector]
    public bool pathIsEnded = false;
    private bool enm_FacingRight;
    public bool playerSpotted = false;

    // The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 3;

    // The waypoint we are currently moving towards
    private int currentWaypoint = 0;

    private bool searchingForPlayer = false;

    void Start()
    {
        enemyGraphics = transform.Find("GhoulGraphics");
        if (enemyGraphics == null)
        {
            enemyGraphics = transform.Find("BatGraphics");
        }

        sceneDirector = GameObject.Find("SceneDirector").GetComponent<SceneDirector>();
        Anim = GetComponentInChildren<Animator>();
        spotDistance = GameObject.Find("SpotDistance").GetComponent<CircleCollider2D>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(9, 10);
        playerSpotted = false;
        enm_FacingRight = true;

        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                //StartCoroutine(SearchForPlayer());

            }

            StartCoroutine(UpdatePath());
            // Start a new path to the target position, return the result to the OnPathComplete method
            //seeker.StartPath(transform.position, target.position, OnPathComplete);

            return;
        }


    }

    IEnumerator SearchForPlayer()
    {
        GameObject sResult = GameObject.FindWithTag("Player");
        if (sResult == null)
        {
            Anim.SetBool("isMoving", false);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchForPlayer());
        }
        else if (sResult && playerSpotted)
        {
            Anim.SetBool("isMoving", true);
            //Debug.Log("found player");
            target = sResult.transform;
            searchingForPlayer = false;
            StartCoroutine(UpdatePath());
            yield return false;
        }
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
            yield return false;
        }

        else
        { 
            // Start a new path to the target position, return the result to the OnPathComplete method
            seeker.StartPath(transform.position, target.position, OnPathComplete);

            yield return new WaitForSeconds(1f / updateRate);
            StartCoroutine(UpdatePath());
        }

    }

    public void OnPathComplete(Path p)
    {
        //Debug.Log("We got a path. Did it have an error? " + p.error);
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        prevPos = rb.position.x;


        if (target == null)
            {
            if (!searchingForPlayer)
                {
                    searchingForPlayer = true;
                    StartCoroutine(SearchForPlayer());
                }

                return;
            }


        if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                if (pathIsEnded)
                    return;

                //Debug.Log("End of path reached.");
                pathIsEnded = true;
            return;
            }
            pathIsEnded = false;

            //Direction to the next waypoint
            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            dir *= speed * Time.fixedDeltaTime;

            //Move the AI
            rb.AddForce(dir, fMode);

        if (dir.x > 0.1 && !enm_FacingRight)
        {
            Flip();
            //Debug.Log("facing right" + dir);
        }
        if (dir.x < 0.1 && enm_FacingRight)
        {
            Flip();
            //Debug.Log("facing left" + dir);
        }

            float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (dist < nextWaypointDistance)
            {
                currentWaypoint++;
                return;
            }
        }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && sceneDirector.isInCutscene == false)
        {
            playerSpotted = true;
            StartCoroutine(SearchForPlayer());
            //Debug.Log("Player spotted;");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerSpotted = false;
            StopCoroutine(SearchForPlayer());
        }
    }

    void Flip()
    {
        enm_FacingRight = !enm_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = enemyGraphics.localScale;
        theScale.x *= -1;
        enemyGraphics.localScale = theScale;
    }
}

