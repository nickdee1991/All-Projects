using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSimpleMovement : MonoBehaviour
{
    public float movementSpeed = 3;
    public int rotationSpeed = 30;

    public bool canJump;
    public float jumpSpeed;

    private Animator anim;
    private Rigidbody rb;
    private AudioManager aud;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        aud = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        #region Player Movement

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * -movementSpeed * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * movementSpeed * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            aud.PlaySound("jump");
            rb.AddForce(Vector2.up * jumpSpeed);
        }

        if (Input.anyKey && anim != null)
        {
            anim.SetBool("moving", true);
        }else{
            anim.SetBool("moving", false);
        }
        #endregion End Player Movement
    }
}
