using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // vertical string grabs value from keypress W and S
    private string moveInputAxis = "Vertical";
    // horizontal string grabs value from keypress A and D
    private string turnInputAxis = "Horizontal";

    //speed at which player rotates
    public float rotationRate = 360;

    //speed at which player will move
    public float moveSpeed = 2;

    #region PlayerController

    // Update is called once per frame
    void Update ()
    {
        //taking value moveInputAxis and turnInputAxis and converting to float value
        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(turnInputAxis);

        ApplyInput(moveAxis, turnAxis);
    }

    
    // call the other values based on what the axis returns
    private void ApplyInput(float moveInput, 
                            float turnInput)
    {
        Move(moveInput);
        Turn(turnInput);
    }

    //defining the move function and the direction the character will move
    private void Move(float input)
    {
        transform.Translate(Vector3.forward * input * moveSpeed);
    }

    //defining the turn function and the rotation of the character
    private void Turn(float input)
    {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }

    #endregion
}
