using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;


[RequireComponent(typeof(PlatformerCharacter2D))]
public class Weapon : MonoBehaviour
{

    RaycastHit2D foundHit;

    public float fireRate = 0;
    public int Damage = 25;
    public LayerMask whatToHit;

    [SerializeField]
    private GameObject Player;

    public Transform BulletTrailPrefab;
    public Transform HitPrefab;
    public Transform MuzzleFlashPrefab;
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    private float jumpForce;

    private bool isDoubleJumping = false;

    //Handle camera shaking
    public float camShakeAmt = 0.05f;
    public float camShakeLength = 0.1f;
    CameraShake camShake;

    public string weaponShootSound = "DefaultShot";

    float timeToFire = 0;
    Transform firePoint;

    //Caching
    AudioManager audioManager;

    // Use this for initialization
    void Awake()
    {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No firepoint= WHAT?!");
        }

    }


    private void Start()
    {
        jumpForce = Player.GetComponent<PlatformerCharacter2D>().m_JumpForce;
        camShake = GameMaster.gm.GetComponent<CameraShake>();
        if (camShake == null)
            Debug.LogError("No CameraShake found on GM objects");

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("Freak out no audiomanager found in scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlatformerCharacter2D>().m_Grounded == true)
        {
            isDoubleJumping = false;
        }

        if (fireRate == 0)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }

        // check for player grounding for topscreen function
        if (Player.GetComponent<PlatformerCharacter2D>().m_Grounded == true)
        {
            Player.GetComponent<Rigidbody2D>().drag = 0;
        }
    }
    #region //SHOOT METHOD
    void Shoot()
    {
        //creating a new vector and assigning new screentoworld point to x and y - translate mouse position from screen coords into game world
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        //taking tip of weapons and storing its position as vector2
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);

        // first attempt to reduce player drag by hitting TopScreen collider with ray mouse1
        // check whether mouse1 is held down
        // and retreving the ground check from PlatformerCharacter2D to see if player is grounded
        if (hit.collider.name == ("TopScreen") && (Input.GetMouseButton(0)) == true && (Player.GetComponent<PlatformerCharacter2D>().m_Grounded == false))
        {
            Player.GetComponent<Rigidbody2D>().drag = 10;
            Debug.Log("raycast 'hit' has collided with " + hit.collider.name);
        }
        else if (Player.GetComponent<PlatformerCharacter2D>().m_Grounded == true)
        {
            Player.GetComponent<Rigidbody2D>().drag = 0;
        }

        // if player raycast meets bottomscreen collider perform jump mid air
        if (hit.collider.name == ("BottomScreen") && (Input.GetMouseButton(0)) == true && (Player.GetComponent<PlatformerCharacter2D>().m_Grounded == false) && isDoubleJumping == false)
        {
            //Player.GetComponent<PlatformerCharacter2D>().Move() = true;
            isDoubleJumping = true;
            Debug.Log("raycast 'hit' has collided with " + hit.collider.name);
            Player.GetComponent<PlatformerCharacter2D>().m_Grounded = false;
            Player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce + (jumpForce /2)));
        }
        else if (Player.GetComponent<PlatformerCharacter2D>().m_Grounded == true)
        {
            Player.GetComponent<Rigidbody2D>().drag = 0;
        }

        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.DamageEnemy(Damage);
                Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage ");
            }

        }

        if (Time.time >= timeToSpawnEffect)
        {
            Vector3 hitPos;
            Vector3 hitNormal;

            if (hit.collider == null)
            {
                hitPos = (mousePosition - firePointPosition) * 30;
                hitNormal = new Vector3(99999, 9999, 9999);
            }
            else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }



            //Effect(hitPos, hitNormal);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
    }
    #endregion
}

//void Effect(Vector3 hitPos, Vector3 hitNormal)
//{
//    Transform trail = Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation) as Transform;
//    LineRenderer lr = trail.GetComponent<LineRenderer>();

//    if (lr != null)
//    {
//        lr.SetPosition(0, firePoint.position);
//        lr.SetPosition(1, hitPos);
//    }

//    Destroy(trail.gameObject, 0.2f);

//    if (hitNormal != new Vector3(9999, 9999, 9999))
//    {
//        Transform hitParticle = Instantiate(HitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal)) as Transform;
//        Destroy(hitParticle.gameObject, 1f);
//    }

//    Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
//    clone.parent = firePoint;
//    float size = Random.Range(0.6f, 0.9f);
//    clone.localScale = new Vector3(size, size, size);
//    Destroy(clone.gameObject, 0.02f);

// Shake the camera
//camShake.Shake(camShakeAmt, camShakeLength);

// Play.ShootSound
//audioManager.PlaySound(weaponShootSound);
//    }
//}