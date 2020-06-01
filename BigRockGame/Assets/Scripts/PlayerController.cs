using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//(Mathf.Abs(charCont.velocity.z) > 1) // placeholder checking change in velcocity against object

public class PlayerController : MonoBehaviour
{
    private AudioManager Aud;
    public AudioSource[] AudGroup;
    int size;

    private Rigidbody rb;
    private CharacterController charCont;
    public Animator anim;

    public float defaultRunSpeed;
    public float runSpeed = 6.0f;
    public float sneakSpeed = 3.0f;
    public float sprintSpeed = 10.0f;
    public float jumpSpeed = 8.0f;

    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        Aud = FindObjectOfType<AudioManager>();
        size = Random.Range(0, AudGroup.Length);
        defaultRunSpeed = runSpeed;
        rb = GetComponentInChildren<Rigidbody>();
        charCont = GetComponentInChildren<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Aud.PlaySound("FistBeat");
            anim.SetBool("isTaunting", true);
        }else{
            anim.SetBool("isTaunting", false);
        }

        void Move()
        {
            if (charCont.isGrounded)
            {
                // We are grounded, so recalculate
                // move direction directly from axes

                moveDirection = transform.TransformDirection(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
                moveDirection *= runSpeed;

                runSpeed = defaultRunSpeed;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                    anim.SetBool("isJumping", true);
                }else{
                    anim.SetBool("isJumping", false);
                }
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            moveDirection.y -= gravity * Time.deltaTime;

            // Move the controller
            charCont.Move(moveDirection * Time.deltaTime);


            if (Input.GetAxis("Vertical") > 0.1)
            {
                anim.SetFloat("Vertical_Moving", Input.GetAxis("Vertical"));
                //Aud.PlaySound("Footstep1"); // grab 3 footstep sounds and play them randomly?

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    runSpeed = sprintSpeed;
                    anim.SetBool("wildRun", true);
                }else{
                    anim.SetBool("wildRun", false);
                }

                if (Input.GetKey(KeyCode.C))
                {
                    runSpeed = sneakSpeed;
                    anim.SetBool("isSneaking", true);
                }else{
                    anim.SetBool("isSneaking", false);
                }
            }

            if (Input.GetAxis("Horizontal") > 0)
            {
                anim.SetFloat("Horizontal_Moving", Input.GetAxis("Horizontal"));
            }
        }
    }
}
