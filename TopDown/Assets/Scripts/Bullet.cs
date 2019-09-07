using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Transform bulletHitWall;
    public Transform bulletHitPlayer;
    public ParticleSystem bulletHole;
    private AudioSource audioSource;

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
        Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<SphereCollider>());
        audioSource = GetComponent<AudioSource>();
    }

    //Method
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        maxDist += 1 * Time.deltaTime;

        if (maxDist >= 5)
            Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        audioSource.Play();

        if (other.gameObject.tag == "Enemy")
        {
            //enemy is hit and health is reduced
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<Guard>().health -= damage;
            Destroy(gameObject);
            Debug.Log("hit " + other.name);
        }

        if (other.gameObject.tag == "Obstacle")
        {
            //if bullet hits wall, destroy bullet and spawn effect TODO: destory effects after time/spawn no.
            bulletHitWall = Instantiate(wallParticle.transform, transform.position, Quaternion.identity);
            Destroy(this);
            bulletHolesSpawned++;

            if (bulletHolesSpawned >= 5)
            {
                Destroy(bulletHole);
            }
        }

        if (other.gameObject.tag == "Player")
        {
            audioSource.Play();
            //player is hit and health is reduced
            player.GetComponent<Player>().health -= 20;
            Destroy(gameObject);
            //bulletHitPlayer = Instantiate(playerHitParticle.transform, other.transform.position, Quaternion.identity);
            print("Colliding with" + other.gameObject.GetComponent<Collider>());

            if (player.GetComponent<Player>().health <= 0)
                player.GetComponent<Player>().Die();
        }
    }
}

