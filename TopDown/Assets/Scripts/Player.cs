using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float sneakSpeed = 3f;
    private float sprintSpeed = 1.5f;
    private float wheelRotation = 500;

    private bool isGrounded;
    public bool isSneaking;

    public GameObject playerObj;
    public GameObject bullet;
    public GameObject bulletSpawnPoint;
    public GameObject playerWheel;

    private Transform bulletSpawn;
    private Rigidbody rb;
    public Camera cam;
    public Vector3 jump;

    //Methods
    private void Start()
    {
        isGrounded = true;

        rb = GetComponent<Rigidbody>();
        jump = new Vector3(transform.position.x, jumpHeight, transform.position.z);

        health = maxHealth;
    }

    private void Update()
    {
        
        #region Player Movement

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            playerWheel.transform.Rotate(new Vector3(0, Time.deltaTime * wheelRotation, 0));
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Sprint();
            } else if (Input.GetKey(KeyCode.LeftControl)) {
                Sneak();
            }

        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
            playerWheel.transform.Rotate(new Vector3(0, Time.deltaTime *- wheelRotation, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {
           transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
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
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (health <= 0)
            Die();
    }

    void Sprint()
    {
        transform.Translate(Vector3.forward * movementSpeed * sprintSpeed * Time.deltaTime);
    }

    void Sneak()
    {
        Debug.Log("I'm Sneaking");
        transform.Translate(Vector3.forward *- sneakSpeed * Time.deltaTime);
        isSneaking = true;
    }

    void Shoot()
    {
        bulletSpawn = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
        bulletSpawn.rotation = bulletSpawnPoint.transform.rotation;
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
    void Die()
    {
        print("you died");
        Destroy(this.gameObject);
        Application.LoadLevel(Application.loadedLevel);
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

    //Player grounded
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            print("grounded");
        } else {
            isGrounded = false;
            print("not grounded");
        }
    }
}
