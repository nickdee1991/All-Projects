using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float movementSpeed;
    public float jumpHeight;
    public float fallingMovement;
    public float sensitivity = 2f;
    public float fallMultiplier = 2.4f;
    public float lowJumpMultiplier = 2f;
    public float waitTime;
    public float points;
    public float maxHealth;
    public float health;

    public float playerWeaponRange = 7.5f;
    private float sneakSpeed = 2.5f;
    private float sprintSpeed = 1f;
    private float wheelRotation = 500;
    private Collider enemyHearRange;

    private bool isGrounded;
    public bool isSneaking;
    private bool isDead;

    public bool hasKeycard = false;
    public bool hasGardenKey = false;
    public bool hasIDcard = false;
    public bool weaponCharged;


    public GameObject taserLED;
    public GameObject weaponEffect;
    public GameObject playerObj;
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public GameObject playerWheel1;
    public GameObject playerWheel2;
    public AudioSource audioShoot;
    public AudioSource audioJump;
    public AudioSource audioLand;
    public AudioSource audioDeath;
    public Light weaponLight;

    private Transform bulletSpawn;
    private Rigidbody rb;
    private Animator anim;
    public Animator deathAnim;
    public Camera cam;
    public Vector3 jump;
    public RaycastHit hitInfo;
    public LineRenderer playerSight;
    public CameraShake cameraShake;

    private void Start()
    {
        Cursor.visible = false;
        taserLED.GetComponent<Renderer>().material.color = Color.green;
        isDead = false;
        weaponCharged = true;
        hasIDcard = false;
        weaponLight.enabled = false;
        audioShoot = GetComponent<AudioSource>();
        playerSight = GetComponent<LineRenderer>();
        anim = GetComponentInChildren<Animator>();
        enemyHearRange = GetComponent<SphereCollider>();
        isSneaking = false;
        rb = GetComponent<Rigidbody>();
        Vector3 vel = rb.velocity;
        jump = new Vector3(transform.position.x, jumpHeight, transform.position.z);

        health = maxHealth;
    }

    public void FixedUpdate()
    {
        #region Player Movement

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            playerWheel1.transform.Rotate(new Vector3(0, 0, Time.deltaTime * -wheelRotation));
            playerWheel2.transform.Rotate(new Vector3(0, 0, Time.deltaTime * -wheelRotation));
            isSneaking = false;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                //StartCoroutine(Sprint());
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime); 
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
            playerWheel1.transform.Rotate(new Vector3(0, 0, Time.deltaTime * wheelRotation));
            playerWheel2.transform.Rotate(new Vector3(0, 0, Time.deltaTime * wheelRotation));
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            transform.Rotate(Vector3.up * -movementSpeed * 15 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.C))
        {
            transform.Rotate(Vector3.up * movementSpeed * 15 * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Jump();
        }

        #region Jump velocity controller
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        #endregion End Jump velocity controller

        #endregion End Player Movement

        #region PlayerShooting
        //shooting
        if (Input.GetMouseButton(1) )
        {
            anim.SetBool("WeaponHolster", false);
            anim.SetBool("WeaponDeploy", true);
            weaponLight.enabled = true;
            //Debug.Log("WEAPON PREPARED");
            if (Input.GetMouseButtonDown(0) && weaponCharged == true)
            {
                Shoot();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("WeaponDeploy", false);
        }

            if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("WeaponDeploy", false);
            anim.SetBool("WeaponHolster", true);
            weaponLight.enabled = false;
        }
        else
        {
            anim.SetBool("WeaponHolster", false);
        }

#endregion

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (health <= 0)
        {
            Die();
        }
    }



    IEnumerator Sprint()
    {
        transform.Translate(Vector3.forward * movementSpeed * sprintSpeed * Time.deltaTime);
        yield return new WaitForSeconds(1);
        StopCoroutine(Sprint());

    }

    void Sneak()
    {
        //Debug.Log("I'm Sneaking");
        movementSpeed = sneakSpeed;
        isSneaking = true;
    }

    void Shoot()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.forward.normalized, out hitInfo, playerWeaponRange))
        {
            // if weapon hits a target
            if (hitInfo.collider != null)
            {
                //StartCoroutine(cameraShake.Shake(.025f, .005f));
                audioShoot.Play();
                bulletSpawn = Instantiate(weaponEffect.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
                Destroy(bulletSpawn.gameObject, .5f);
                Debug.DrawRay(bulletSpawnPoint.transform.position, hitInfo.point, Color.green);
                Debug.Log("Shooting at " + hitInfo.transform.name);

                // weapon hit enemy
                if (hitInfo.transform.gameObject.CompareTag("Enemy"))
                {
                    print("Hit enemy");
                    hitInfo.collider.gameObject.GetComponent<GuardPatrol>().Die();
                    weaponCharged = false;
                    taserLED.GetComponent<Renderer>().material.color = Color.red;
                }
                if (hitInfo.transform.gameObject.CompareTag("Fusebox"))
                {
                    GameObject.FindGameObjectWithTag("Fusebox").GetComponent<fuseboxinteractable>().fuseBoxDestroyed = true;
                    Debug.Log("shooting at fusebox");
                }
            }
        }else{
            {
                //If don't hit, then draw red line to the direction we are sensing,
                //Note hit.point will remain 0,0,0 at this point, because we don't hit anything
                //So you cannot use hit.point
                Debug.DrawLine(bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.forward + (fwd * playerWeaponRange), Color.red);
            }
        }
    }

    void Jump()
    {
        audioJump.Play();
        isGrounded = false;
        rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
        Vector3 velocity = rb.velocity;
        velocity.y += jumpHeight;
        rb.velocity = velocity;
    }

    //player death and move camera to enemy
    public void Die()
    {
        if (isDead == false)
        {
            audioDeath.Play();
            StartCoroutine(IsDead());
            isDead = true;
        } 
    }

    IEnumerator IsDead()
    {        
        deathAnim.SetBool("isDead", true);
        print("you died");
        Destroy(gameObject, 5f);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CameraScreenPointToRay()
    {
        //player facing mouse
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);
        }
    }

    //sneaking trigger
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isSneaking == false)
        {
            Debug.Log("Footsteps Heard");
            other.GetComponentInChildren<GuardPatrol>().Attack();
        }
    }

    //Player grounded
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Obstacle" && isGrounded == false)
        {
            isGrounded = true;
            if (other.gameObject.tag == "Ground" && isGrounded == true)
            {
                audioLand.Play();
            }
           else if (isGrounded != true)
            {
                {
                    isGrounded = false;
                }
            }


        }
    }
}
