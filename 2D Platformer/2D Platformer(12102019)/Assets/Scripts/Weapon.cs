using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets._2D;
//using Pathfinding.RVO;
using Pathfinding.Util;


[RequireComponent(typeof(PlatformerCharacter2D))]
public class Weapon : MonoBehaviour
{

    public enum PlayerState {Idle, Moving, Jumping, Sliding, Gliding, Swinging, Falling, Attacking, Dead }
    public PlayerState CurrentState;

    public float fireRate = 0;
    public float Damage;
    public float PushbackAmount;
    public float Range = 10;
    public LayerMask whatToHit;

    [SerializeField]
    private GameObject Player;
    private GameObject gm; // reference to the gamemaster object
    private Rigidbody2D rb;
    private DistanceJoint2D distJoint;
    public BoxCollider2D weaponBlock;
    public LineRenderer lr;
    public SpriteRenderer weaponGraphics;
    public SpriteRenderer weaponHandle;
    public SpriteRenderer weaponSwing;

    public Transform BulletTrailPrefab;
    public Transform HitPrefab;
    public Transform MuzzleFlashPrefab;
    public float effectSpawnRate = 10;
    private float jumpForce;
    public float weaponDeployFall = 10;
    private float windUpTime = 8;


    public bool isDoubleJumping;
    public bool isGliding;
    public bool PlayerGrab = false; // check for if the player is colliding with the HookInteractable
    public bool weaponDeployed;
    public bool isTopCollider;
    public bool isBottomCollider;
    public bool isLeftCollider;
    public bool isRightCollider;

    //Handle camera shaking
    public float camShakeAmt = 0.05f;
    public float camShakeLength = 0.1f;
    CameraShake camShake;

    public string weaponShootSound = "DefaultShot";

    Transform firePoint;

    //Caching
    AudioManager audioManager;
    Animator anim;

    // Use this for initialization
    void Awake()
    {
        firePoint = transform.Find("FirePoint");

        if (firePoint == null)
        {
            Debug.LogError("No firepoint = WHAT?!");
        }
    }

    void Start()
    {
        //Physics2D.IgnoreLayerCollision(11,14, true);
        weaponDeployed = false;
        PlayerGrab = false;
        isGliding = false;
        isTopCollider = false;
        isBottomCollider = false;
        isLeftCollider = false;
        isRightCollider = false;
        Player = GameObject.FindWithTag("Player");
        anim = GetComponentInChildren<Animator>();
        jumpForce = Player.GetComponent<PlatformerCharacter2D>().m_JumpForce;
        camShake = GameMaster.gm.GetComponent<CameraShake>();
        gm = GameObject.FindGameObjectWithTag("GM");
        camShake = gm.GetComponent<CameraShake>();
        rb = GetComponent<Rigidbody2D>();
        distJoint = GetComponentInParent<DistanceJoint2D>();
        lr = GetComponent<LineRenderer>();
        weaponGraphics = GameObject.Find("WeaponGraphics").GetComponent<SpriteRenderer>();
        weaponHandle = GameObject.Find("WeaponHandle").GetComponent<SpriteRenderer>();
        weaponSwing = GameObject.Find("WeaponSwing").GetComponent<SpriteRenderer>();
        weaponBlock = GameObject.Find("WeaponBlock").GetComponent<BoxCollider2D>();
        weaponBlock.enabled = false;
        weaponHandle.enabled = false;
        weaponSwing.enabled = false;
        distJoint.enabled = false;
        lr.enabled = false;
        if (camShake == null)
        {
            Debug.LogError("No CameraShake found on GM objects");
        }

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            Debug.LogError("Freak out no audiomanager found in scene");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(CurrentState); //show what the current state is
        #region Umbrella Situational Control (top,bottom,left,right)
        //creating a new vector and assigning new screentoworld point to x and y - translate mouse position from screen coords into game world
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        //taking tip of weapons and storing its position as vector2
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, Range, whatToHit);

        CurrentState = PlayerState.Idle;

        if ((Input.GetMouseButton(0))) // if umbrella is deployed
        {
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null && !PlayerGrab)
            {
                lr.SetPosition(0, firePointPosition);
                lr.SetPosition(1, hit.point);
                CurrentState = PlayerState.Swinging;
            }

            if (hit.collider == null && PlayerGrab == false)
            {
                Debug.DrawLine(firePointPosition, mousePosition, Color.red);
                //lr.enabled = true;
                //lr.SetPosition(0, firePointPosition);
                //lr.SetPosition(1, mousePosition);
                //Effect(firePointPosition, mousePosition - firePointPosition);
                CurrentState = PlayerState.Idle;
            } // if hook is shot out and no target CurrentState = PlayerState.Idle

            switch (CurrentState)
            {
                case PlayerState.Swinging: // player has attached to a swingable object

                    PlayerGrab = true;
                    distJoint.enabled = true;
                    distJoint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                    distJoint.distance = Vector2.Distance(hit.point, Player.transform.position);

                    weaponHandle.enabled = true;
                    weaponSwing.enabled = true;
                    weaponGraphics.enabled = false;
                    lr.enabled = true;
                    lr.SetPosition(0, firePointPosition);
                    lr.SetPosition(1, hit.point);
                    weaponHandle.transform.parent = hit.transform.parent;
                    weaponHandle.transform.rotation = transform.rotation;
                    weaponHandle.transform.position = hit.point;

                    if (distJoint.distance <= Range)
                    {
                        Debug.DrawLine(firePointPosition, hit.point, Color.green);
                        distJoint.distance = Range; //maybe TODO - add mouse1 hold to wind up rope (distJoint.distance = Range -- *time.deltatime; if range <= 2.5f range = 2.5f)
                        Player.GetComponent<PlatformerCharacter2D>().currentSpeed = Player.GetComponent<PlatformerCharacter2D>().m_MaxSpeed;
                    }
                    break;

                case PlayerState.Gliding: // player has deployed umbrella upward - slowing descent
                    //do the things
                    break;
                case PlayerState.Attacking: // player is moving umbrella quickly - storing velocity as damage
                    //do the things
                    break;
                case PlayerState.Idle: // player is doing nothing
                    //do the things
                    break;

            }
        }
        if ((Input.GetMouseButton(0)))
        {
            lr.SetPosition(0, firePointPosition);
            if (hit.collider == null && PlayerGrab == false)
            {
                Debug.DrawLine(firePointPosition, mousePosition, Color.red);
                //lr.enabled = true;
                //lr.SetPosition(0, firePointPosition);
                //lr.SetPosition(1, mousePosition);
                //Effect(firePointPosition, mousePosition - firePointPosition);
            }


            if (Input.GetKey(KeyCode.W)&& PlayerGrab == true)
            {
                Debug.Log("wind that shit up " + distJoint.distance);
                distJoint.distance -= Time.deltaTime * windUpTime;
            } // winding up grapple
            if (Input.GetKey(KeyCode.S) && PlayerGrab == true)
            {
                Debug.Log("unwind that shit " + distJoint.distance);
                distJoint.distance += Time.deltaTime * windUpTime;
            } // unwinding grapple
        }
            #endregion
        


        // check for player grounding for weaponCollider functions
        if (Player.GetComponent<PlatformerCharacter2D>().m_Grounded)
        {
            isDoubleJumping = false;
            isBottomCollider = false;
            isTopCollider = false;
            Player.GetComponent<Rigidbody2D>().drag = 0;
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            //CurrentState = PlayerState.Idle;
            lr.enabled = false;
            //if player is attached to hook and leaves. perform below
            if (PlayerGrab == true)
            {
                weaponBlock.enabled = false;
                //rb.AddRelativeForce(transform.InverseTransformDirection(rb.velocity) * 100);    // not even sure if this is working - supposed to be applying force relative to the direction player is heading
                weaponGraphics.enabled = true;
                distJoint.enabled = false;
                weaponHandle.enabled = false;
                weaponSwing.enabled = false;
                weaponHandle.transform.parent = gameObject.transform.parent;
                lr.enabled = false;
                PlayerGrab = false;
                isDoubleJumping = false;
            }
                #region gameobjects disabling for hook action
            //if player releases 0 - player fall etc
            anim.SetBool("isGliding", false);
            audioManager.PlaySound("UmbrellaClose");
            Player.GetComponent<Rigidbody2D>().drag = 0;
            isGliding = false;
            weaponDeployed = false;
            weaponBlock.enabled = false;
            #endregion
        }
    }

    //if player umbrella hits enemy - they are pushed backward
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (weaponDeployed)
            {
                Vector2 moveDirection = transform.position - collision.transform.position.normalized;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(moveDirection * PushbackAmount, ForceMode2D.Force);
                //collision.gameObject.GetComponent<Rigidbody2D>().freeze;
                Debug.Log("umbrella collided with " + collision.gameObject.name + "by amount " + PushbackAmount);
                //collision.gameObject.GetComponent<Enemy>().DamageEnemy(999);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if ((Input.GetMouseButton(1)) )
        {
            Physics2D.IgnoreLayerCollision(14, 11, true);
            weaponDeployed = true;
            weaponBlock.enabled = true;
            anim.SetBool("isGliding", true);
            audioManager.PlaySound("UmbrellaOpen");

            if (Player.GetComponent<PlatformerCharacter2D>().m_Grounded == false)
            {
                //Debug.Log("weapon colliding with " + collision.gameObject.name);
                if (collision.gameObject.CompareTag("TopCollider"))
                {
                    //Debug.Log("has collided with top collider");
                    isGliding = true;
                    isTopCollider = true;       //hate adding bool to this but needs to be done TODO: deprecate this
                    Player.GetComponent<Rigidbody2D>().drag = weaponDeployFall;
                }
                if (isDoubleJumping == false)
                {
                    if (collision.gameObject.CompareTag("BottomCollider")) // && !collision.gameObject.layer.Equals("WeaponBlock") - does this need to be in?
                    {
                        //Debug.Log("has collided with bottom collider");
                        //Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
                        Player.GetComponent<PlatformerCharacter2D>().m_Grounded = false;
                        Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce + (jumpForce / 1.5f)));
                        isDoubleJumping = true;
                        //isBottomCollider = true;
                    }
                }
            }
        }
    }

    //if player umbrella leaves top collider - player will fall
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TopCollider"))
        {
            //Debug.Log("has collided with top collider");
            isGliding = false;
            isTopCollider = false;       //hate adding bool to this but needs to be done TODO: deprecate this
            Player.GetComponent<Rigidbody2D>().drag = 0;
        }
    }

    void Effect(Vector3 hitPos, Vector3 hitNormal)
    {
        Transform trail = Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if (lr != null)
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos);
        }

        Destroy(trail.gameObject, 0.2f);

        if (hitNormal != new Vector3(9999, 9999, 9999))
        {
            Transform hitParticle = Instantiate(HitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal)) as Transform;
            Destroy(hitParticle.gameObject, 0.5f);
        }

        Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);

        //Shake the camera

    //camShake.Shake(camShakeAmt, camShakeLength);

        //Play.ShootSound

        //audioManager.PlaySound(weaponShootSound);
    }
}



