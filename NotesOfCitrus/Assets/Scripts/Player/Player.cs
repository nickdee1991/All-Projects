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

    private Collider enemyHearRange;

    private bool isGrounded;
    public bool isSneaking;
    private bool isDead;


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

    private void Start()
    {
        Cursor.visible = false;;
        weaponLight.enabled = false;
        audioShoot = GetComponent<AudioSource>();
        playerSight = GetComponent<LineRenderer>();
        anim = GetComponentInChildren<Animator>();
        enemyHearRange = GetComponent<SphereCollider>();
        isSneaking = false;
        rb = GetComponent<Rigidbody>();
        Vector3 vel = rb.velocity;
        jump = new Vector3(transform.position.x, jumpHeight, transform.position.z);
    }

    public void FixedUpdate()
    {
        #region Player Movement

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
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

        #endregion End Player Movement

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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
