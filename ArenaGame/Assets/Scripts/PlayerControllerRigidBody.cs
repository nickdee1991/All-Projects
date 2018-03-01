using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRigidBody : MonoBehaviour {

    // vertical string grabs value from keypress W and S
    private string moveInputAxis = "Vertical";
    // horizontal string grabs value from keypress A and D
    private string turnInputAxis = "Horizontal";

    public float Run;
    public Animator anim;

    public bool isGrounded;

    //speed at which player rotates
    public float rotationRate = 360;

    private Rigidbody rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    //speed at which player will move
    public float moveSpeed = 10;

    #region PlayerController

    // Update is called once per frame
    void Update ()
    {
        

        //taking value moveInputAxis and turnInputAxis and converting to float value
        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(turnInputAxis);


        ApplyInput(moveAxis, turnAxis);

        //bool Throw = Input.GetMouseButtonDown(0);

        //anim.SetBool("Throw", Throw);

        //float Run = Input.GetAxis("Vertical");

        //anim.SetFloat("InputX", Run);
    }

    
    // call the other values based on what the axis returns
    private void ApplyInput(float moveInput, 
                            float turnInput)
    {
        Move(moveInput);
        Turn(turnInput);
    }

    //defining the move function and the direction the character will move
    public void Move(float input)
    {

        rb.AddForce(transform.forward * input * moveSpeed, ForceMode.Force);
        //transform.Translate(Vector3.forward * input * moveSpeed);
    }

    //defining the turn function and the rotation of the character
    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }

    #endregion
}
