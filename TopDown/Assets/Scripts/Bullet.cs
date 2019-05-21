using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Transform bulletHitWall;
    public Transform bulletHitPlayer;
    public ParticleSystem bulletHole;

    public float speed;
    public float maxDist;
    public float damage;
    private float bulletHolesSpawned;

    private GameObject triggeringEnemy;
    public GameObject wallParticle;
    public GameObject playerHitParticle;

    private GameObject player;


    //Variables
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //Method
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        maxDist += 1 * Time.deltaTime;

        if (maxDist >= 5)
            Destroy(this.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //enemy is hit and health is reduced
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<Enemy>().health -= damage;
            Destroy(this.gameObject);
            Debug.Log("hit enemy");
        }

        if (other.gameObject.tag == "Obstacle")
        {
            //Debug.Log("hit wall");

            //if bullet hits wall, destroy bullet and spawn effect TODO: destory effects after time/spawn no.
            bulletHitWall = Instantiate(wallParticle.transform, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            bulletHolesSpawned++;

            if (bulletHolesSpawned >= 5)
            {
                Destroy(bulletHole);
            }
        }

        if (other.gameObject.tag == "Player")
        {
            //player is hit and health is reduced
            player.GetComponent<Player>().health -= 20;
            Destroy(this.gameObject);
            //bulletHitPlayer = Instantiate(playerHitParticle.transform, other.transform.position, Quaternion.identity);
            Debug.Log("player hit");

            if (player.GetComponent<Player>().health <= 0)
                player.GetComponent<Player>().Die();
        }
    }
}

