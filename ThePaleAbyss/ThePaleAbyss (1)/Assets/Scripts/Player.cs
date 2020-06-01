using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float movementSpeed;
    public float pauseSpeed = 0f;
    public float sensitivity = 2f;
    public float waitTime;
    public float rotationSpeed = 5f;

    public bool noCoat;
    public bool isWalking;
    public bool isInteracting;
    public bool canMoveZ;

    public GameObject playerObj;
    public GameObject playerGraphics;
    public Animator anim;
    public AudioManager audioMgr;
    private InteractableManager intMgr;
    private LevelDirector levelDirector;

    public Light flashlight;
    private Rigidbody rb;
    public Camera cam;
    public RaycastHit hitInfo;

    private void Start()
    {
        audioMgr = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        canMoveZ = false;
        isWalking = false;
        isInteracting = false;
        playerGraphics = GameObject.Find("PlayerGraphics");
        anim = playerGraphics.GetComponent<Animator>();
        intMgr = FindObjectOfType<InteractableManager>();
        flashlight = GameObject.Find("Flashlight").GetComponent<Light>();
        levelDirector = FindObjectOfType<LevelDirector>();
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        Vector3 vel = rb.velocity;
    }
    public void Update()
    {
        #region Player Movement

        if (isInteracting)
        {
            if (noCoat)
            {
                anim.SetBool("noCoat", true);
            }
            anim.SetBool("isInteracting",true);
        }else{
            if (noCoat)
            {
                anim.SetBool("noCoat", true);
            }
            anim.SetBool("isInteracting", false);
        }

        if (!isWalking)
        {
            if (noCoat)
            {
                anim.SetBool("noCoat", true);
            }
            anim.SetBool("isWalking", false);
        }
        isWalking = false;
        isInteracting = false;

        if (Input.GetKey(KeyCode.V))
        {
            levelDirector.GameOver = true;
            levelDirector.PlayerDead();
        }

        if (Input.GetKey(KeyCode.W) && canMoveZ == true)
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            if (noCoat)
            {
                anim.SetBool("noCoat", true);
            }
            if (intMgr.inBasement)
            {
                anim.SetBool("inBasementWalk", true);
            }
            anim.SetBool("isWalking", true);
            isWalking = true;
        }else{
            anim.SetBool("inBasementWalk", false); // when player is not moving in basement set the walking bool to false
        }        

        if (Input.GetKey(KeyCode.A))
        {
            if (!intMgr.inBasement)
            {
                transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
                playerGraphics.GetComponent<SpriteRenderer>().flipX = true;
            }else{
                transform.Rotate(Vector3.up * -movementSpeed * rotationSpeed * Time.deltaTime);
            }

            if (noCoat)
            {
                anim.SetBool("noCoat", true);
            }
            anim.SetBool("isWalking", true);
            isWalking = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (!intMgr.inBasement)
            {
                transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
                playerGraphics.GetComponent<SpriteRenderer>().flipX = false;
            }else{
                transform.Rotate(Vector3.up * movementSpeed * rotationSpeed * Time.deltaTime);
            }

            if (noCoat)
            {
                anim.SetBool("noCoat", true);
            }
            anim.SetBool("isWalking", true);
            isWalking = true;
        }

        if (Input.GetKey(KeyCode.S) && canMoveZ == true)
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
            if (noCoat)
            {
                anim.SetBool("noCoat", true);
            }
            anim.SetBool("isWalking", true);
            isWalking = true;
        }

        #endregion End Player Movement

    }
}
