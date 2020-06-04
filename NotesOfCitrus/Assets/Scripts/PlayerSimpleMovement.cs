using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleMovement : MonoBehaviour
{
    public int movementSpeed = 3;
    public int sprintSpeed = 6;
    public int rotationSpeed = 40;
    public Animator anim;

    public SphereCollider noiseRange;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        noiseRange.enabled = false;
    }

    public void Update()
    {
        #region Player Movement

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            anim.SetBool("isMoving", true);
        }else{
            anim.SetBool("isMoving", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
            anim.SetBool("isMoving", true);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 6;
            anim.SetBool("isRunning", true);
            noiseRange.enabled = true;
        }else{
            movementSpeed = 3;
            noiseRange.enabled = false;
            anim.SetBool("isRunning", false);
        }

        #endregion End Player Movement
    }


}
