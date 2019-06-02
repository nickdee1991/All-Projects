using System.Collections;
using System.Collections.Generic;
using UnityEngine;
     
public class Enemy : MonoBehaviour {

    //Variables
    private int patrolSpot;

    public float health;
    public float pointsToGive;
    public float waitTime;
    public float sightDistance;
    public float stopDistance;
    public float patrolSpeed = 2.5f;
    public float startWaitTime;

    private float patrolWaitTime;
    private float currentTime;
    private bool shot;
    private bool isMoving;
    private float wheelRotation = 150;

    public bool detectedEnemy;

    public Transform deathParticleSpawn;
    public Transform bulletSpawnPoint;
    private Transform bulletSpawned;
    private Transform pistolHolder;

    public Transform[] moveSpots;

    public GameObject player;
    public GameObject deathParticle;
    public GameObject bullet;
    public GameObject enemyWheel1;
    public GameObject enemyWheel2;

    public Animator animator;

    public LineRenderer enemySight;
    public Gradient redColor;
    public Gradient greenColor;
    public Gradient yellowColor;

    public RaycastHit hitInfo;


    //Methods
    public void Start()
    {
        patrolWaitTime = startWaitTime;
        patrolSpot = (patrolSpot + 1) % moveSpots.Length;

        player = GameObject.FindWithTag("Player");
        pistolHolder = this.transform.GetChild(1);
        bulletSpawnPoint = pistolHolder.GetChild(1);

    }

    public void Update()
    {
        if (isMoving == true)
        {
            enemyWheel1.transform.Rotate(new Vector3(0,  0, Time.deltaTime * wheelRotation));
            enemyWheel2.transform.Rotate(new Vector3(0,  0, Time.deltaTime * wheelRotation));
        }

        //counting down when to shoot
        if (shot && currentTime < waitTime)
        {
            currentTime += 1 * Time.deltaTime;
        }
        if (currentTime >= waitTime)
        {
            currentTime = 0;
        }

        //if there is no bullet spawn point for Enemy - assign one within object
        if (!bulletSpawnPoint)
        {
            pistolHolder = this.transform.GetChild(0);
            bulletSpawnPoint = pistolHolder.GetChild(2);
        }

        //if the health of this object reaches X then destroy and spawn particle effect
        if (health <= 0)
        {
            Die();
        }

        transform.position = Vector3.MoveTowards(transform.position, moveSpots[patrolSpot].position, patrolSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveSpots[patrolSpot].position) < 0.5F)
        {
            PatrolPath();
        }

        #region Enemy LoS
        //sending raycast to look for player
        if (Physics.Raycast(transform.position, transform.forward.normalized, out hitInfo, sightDistance))
        {
            //if the enemy ray hits an object (Obstacle || player)
            if (hitInfo.collider != null)
            {
                enemySight.SetPosition(1, hitInfo.point);
                //enemySight.colorGradient = greenColor;
                //Debug.Log("enemy looking at " + hitInfo.collider);
                Debug.DrawRay(transform.position, hitInfo.point, Color.green);

                if (hitInfo.collider.CompareTag("Player"))
                {
                    isMoving = false;
                    Debug.DrawRay(transform.position, hitInfo.point, Color.red);
                    Attack();
                } else {
                    {
                        detectedEnemy = false;
                        patrolSpeed = 3;
                        //Debug.Log("Detection = " + GetComponent<Patrol>().detectedEnemy);
                        enemySight.SetPosition(1, hitInfo.point);
                        enemySight.colorGradient = greenColor;
                    }
                }
            }
        }
        #endregion Enemy LoS

        enemySight.SetPosition(0, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Enemy: I've been shot!");
            Attack();
        }
    }

    public void PatrolPath()
    {
        if (patrolWaitTime <= 0 && detectedEnemy == false)
        {
            isMoving = true;
            //Debug.Log("Detection = " + detectedEnemy);
            patrolSpot = (patrolSpot + 1) % moveSpots.Length;
            patrolWaitTime = startWaitTime;
            //Debug.Log("moving to position " + patrolSpot);

            //look at next patrol point
            transform.LookAt(moveSpots[patrolSpot]);

            //var lookRot = lookRotationSpeed * Time.deltaTime;

            //var targetRotation = Quaternion.LookRotation(moveSpots[patrolSpot].transform.position - transform.position);

            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookRot);

            //transform.rotation = Quaternion.Lerp(lookStart, lookEnd, lookRot);
        } else {
            patrolWaitTime -= Time.deltaTime;
            isMoving = false;
        }
    }

    public void Die()
    {
        //destroy object, spawn particle effect, give point to player
        print(this.gameObject.name + " has died!");
        deathParticleSpawn = Instantiate(deathParticle.transform, deathParticleSpawn.transform.position, Quaternion.identity);
        Destroy(this.gameObject, 1f);        
        player.GetComponent<Player>().points += pointsToGive;

    }


    public void Attack()
    {
        detectedEnemy = true;
        //red ray
        Debug.DrawRay(transform.position, hitInfo.point, Color.red);

        //enemySight.colorGradient = redColor;

        //look at player
        transform.LookAt(player.transform.position);

        //move towards player
        MoveToTarget();

        #region shoot at player 

        if (currentTime == 0)
        {
            Shoot();
        }

        #endregion

        //if enemy moves too close, stop at a distance until player leaves space
        if (Vector3.Distance(transform.position, player.transform.position) < stopDistance)
        {
            patrolSpeed = 0;
            isMoving = false;
        }
        else
        {
            patrolSpeed = 3;
            MoveToTarget();
        }
    }


    public void Shoot()
    {
        {
            shot = true;

            bulletSpawned = Instantiate(bullet.transform, bulletSpawnPoint.position, Quaternion.identity);
            bulletSpawned.rotation = this.transform.rotation;
        }
    }


    public void MoveToTarget()
    {
        isMoving = true;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, patrolSpeed * 2 * Time.deltaTime);
    }
}
