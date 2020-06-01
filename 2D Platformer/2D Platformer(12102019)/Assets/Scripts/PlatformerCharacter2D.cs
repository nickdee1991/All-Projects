using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {

        public float currentSpeed = 0f;         //the current x moving speed of player
        public float movementSpeed = 5.0f;         //the default movement speed
        public float accelerationTime = 20;         // the rate at which player gains speed
        public float minSpeed = 15f;        //default speed for player when movement has stopped
        private float time; // counting time for movement values to increase/decrease by (basically Time.deltaTime)


        [SerializeField] public float m_MaxSpeed = 30f;                    // The fastest the player can travel in the x axis.
        [SerializeField] public float m_JumpForce;                  // Amount of force added when the player jumps.
        [SerializeField] public float m_JumpForceDefault;
        [SerializeField] public float m_JumpForceSpeed;
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character


        //Player sounds
        [SerializeField]
        string landingSoundName = "LandingFootsteps";

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        public bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        public Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D; // Reference to the Rigidbody2d component attached.
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        public bool isSliding;
        private Weapon weaponCtrl;
        private BoxCollider2D playerStanding;

        public float fallMultiplier = 2.5f;        //the rate at which the player will fall
        public float lowJumpMultiplier = 2f;        //holding higher player will jump whilst holding 'jump' button down
        public float runningJump;           //jump boost that player will get whilst running (close to max speed)
        public float stillJump;             //the default speed for player if stationary/ slow moving

        Transform playerGraphics;  //Reference to Graphics so we can change the direction
        Transform playerArm; // Reference to the playerArm to change direction when body moves

        [SerializeField]
        private GameObject Graphics;        // reference to player sprite used for calling animation along with m_Anim
        public ParticleSystem footStep;

        AudioManager audioManager; // reference to the scene audiomanager - for calling player sounds

        void Awake()
        {
            m_JumpForce = m_JumpForceDefault;
            currentSpeed = minSpeed;
            time = 0;
            //when game starts player is not sliding
            isSliding = false;
            playerStanding = GetComponent<BoxCollider2D>();
            weaponCtrl = GetComponentInChildren<Weapon>();
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            Graphics = GameObject.FindWithTag("Graphics");
            m_Anim = Graphics.gameObject.GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            playerGraphics = transform.Find("Graphics");
            playerArm = transform.Find("Arm");

            //Physics2D.IgnoreLayerCollision(11, 10);
            if (playerGraphics == null)
            {
                Debug.LogError("Let's freak out! There is no 'Graphics' object as a child of player");
            }
        }

        private void Start()
        {
            audioManager = AudioManager.instance;
            if (audioManager == null)
            {
                Debug.LogError("THIS IS WHY WE WRTIE ERROR MESSAGES: NO AUDIOMANAGER FOUND!");
            }
        }

        void FixedUpdate()
        {
            #region Trail renderer timers
            if (Graphics.GetComponentInChildren<TrailRenderer>().time <= 0)
            {
                Graphics.GetComponentInChildren<TrailRenderer>().time = 0.5f;
            }
            if (Graphics.GetComponentInChildren<TrailRenderer>().time >= 30)
            {
                Graphics.GetComponentInChildren<TrailRenderer>().time = 30;
            }

            Graphics.GetComponentInChildren<TrailRenderer>().time -= Time.deltaTime * currentSpeed;
            #endregion

            Vector3 pos = transform.position;
            pos.z = 0;
            transform.position = pos;



            bool wasGrounded = m_Grounded;

            m_Grounded = Physics2D.OverlapCircle(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

            m_Anim.SetBool("Ground", m_Grounded);

            if (!wasGrounded && m_Grounded == true)
            {
                audioManager.PlaySound(landingSoundName);
            }
            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            //Better jump code
            if (m_Rigidbody2D.velocity.y < 0)
            {
                m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (m_Rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }


        public void Move(float move, bool crouch, bool jump)
        {
            if (crouch && currentSpeed > 15)
            {
                playerStanding.enabled = false;
                isSliding = true;
                Instantiate(weaponCtrl.BulletTrailPrefab, m_GroundCheck.position, m_GroundCheck.rotation);
                m_Anim.SetBool("Crouch", true);

                //Vector3 theArmFlip = playerArm.localPosition;
                //theArmFlip.y *= -1;
                //theArmFlip.x *= -1;
                //playerArm.localPosition = theArmFlip;

                Debug.Log("sliding speeed "+ currentSpeed);
                //currentSpeed = currentSpeed -= m_CrouchSpeed * Time.deltaTime;
            }else if (currentSpeed < 15){
                playerStanding.enabled = true;
                isSliding = false;

                //Vector3 theArmFlip = playerArm.localPosition;
                //theArmFlip.y *= 1;
                //theArmFlip.x *= 1;
                //playerArm.localPosition = theArmFlip;
            }
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            if (jump == true)
            {
                Instantiate(weaponCtrl.BulletTrailPrefab, m_GroundCheck.position, m_GroundCheck.rotation);
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                currentSpeed = Mathf.SmoothStep(minSpeed, m_MaxSpeed, time / accelerationTime);
                // - increase player speed as the move in a direction
                if (m_Rigidbody2D.velocity.x > 0.1f || m_Rigidbody2D.velocity.x < -0.1f && crouch == false)
                {
                    //Instantiate(footStep, m_GroundCheck.position, m_GroundCheck.rotation);
                    transform.position -= transform.forward * currentSpeed * Time.deltaTime;
                    time += Time.deltaTime;
                    if (currentSpeed >= 22.5)
                    {
                        m_Anim.SetBool("Sprint", true);
                        Graphics.GetComponentInChildren<TrailRenderer>().time++;
                        lowJumpMultiplier = runningJump;
                        m_JumpForce = m_JumpForceSpeed;
                    }else if (currentSpeed <= 22.5)
                    {
                        m_Anim.SetBool("Sprint", false);
                        lowJumpMultiplier = stillJump;
                        m_JumpForce = m_JumpForceDefault;
                    }
                }else{
                    m_Anim.SetBool("Sprint", false);
                    currentSpeed = minSpeed;
                    time = 0;
                }       //this is so messy doing double else statement - TODO: fix this

                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move * m_CrouchSpeed : move);



                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move * currentSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                //Debug.Log("not grounded");
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy") && isSliding == true)
            {
                    Debug.Log("collided with enemy whilst sliding = add damage");
                    collision.gameObject.GetComponent<Enemy>().DamageEnemy(999);        
            }
        }
        void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            Vector3 theArmScale = playerArm.localScale;
            theArmScale.y *= -1f;
            playerArm.localScale = theArmScale;

            Vector3 theArmFlip = playerArm.localPosition;
            theArmFlip.x *= -1;
            playerArm.localPosition = theArmFlip;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = playerGraphics.localScale;
            theScale.x *= -1;
            playerGraphics.localScale = theScale;
        }
    }
}