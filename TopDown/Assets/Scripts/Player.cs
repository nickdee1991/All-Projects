using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    //Variables
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

    private float sneakSpeed = 2.5f;
    private float sprintSpeed = 1.5f;
    private float wheelRotation = 500;
    private Collider enemyHearRange;

    private bool isGrounded;
    public bool isSneaking;
    public bool hasKeycard = false;

    public GameObject playerObj;
    public GameObject bullet;
    public GameObject bulletSpawnPoint;
    public GameObject playerWheel1;
    public GameObject playerWheel2;

    private Transform bulletSpawn;
    private Rigidbody rb;
    public Camera cam;
    public Vector3 jump;

    //Methods
    private void Start()
    {
        enemyHearRange = this.GetComponent<SphereCollider>();
        isSneaking = false;
        rb = GetComponent<Rigidbody>();
        Vector3 vel = rb.velocity;
        jump = new Vector3(transform.position.x, jumpHeight, transform.position.z);

        health = maxHealth;
    }

    public void Update()
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
                StartCoroutine(Sprint());
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                Sneak();
            }

        }

        if (rb.velocity.magnitude < 0.2)
        {
            //isSneaking = true;
            // do idle animations
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

        //shooting
        if (Input.GetMouseButton(1))
        {
            //Debug.Log("WEAPON PREPARED");
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
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
        //transform.Translate(Vector3.forward * movementSpeed /1 * Time.deltaTime);
        StopCoroutine(Sprint());

    }

    void Sneak()
    {
        Debug.Log("I'm Sneaking");
        transform.Translate(Vector3.forward * -sneakSpeed * Time.deltaTime);
        isSneaking = true;
    }

    void Shoot()
    {
        bulletSpawn = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
        bulletSpawn.rotation = bulletSpawnPoint.transform.rotation;
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), this.GetComponent<SphereCollider>());
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), this.GetComponent<BoxCollider>());
    }


    void Jump()
    {
        rb.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
        isGrounded = false;
        Vector3 velocity = rb.velocity;
        velocity.y += jumpHeight;
        rb.velocity = velocity;
    }

    //player death and move camera to enemy
    public void Die()
    {
        print("you died");
        Destroy(this.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CameraScreenPointToRay()
    {
        //player facing mouse
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
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

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isSneaking == false)
        {
            Debug.Log("Footsteps Heard");
            other.GetComponent<Guard>().Attack();
        }
    }

    //Player grounded
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            //print("grounded");
        } else if (isGrounded != true){
            {
                isGrounded = false;
            }
            //print("not grounded");
        }
    }
}
