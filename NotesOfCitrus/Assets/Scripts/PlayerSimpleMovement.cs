using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleMovement : MonoBehaviour
{
    public int defaultMovementSpeed = 3;

    public int movementSpeed;
    private int stoppedSpeed = 0;
    public int sprintSpeed = 6;
    public Animator anim;

    public SphereCollider noiseRange;

    private void Start()
    {
        movementSpeed = defaultMovementSpeed;
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
            movementSpeed = sprintSpeed;
            anim.SetBool("isRunning", true);
            noiseRange.enabled = true;
        }else{
            movementSpeed = defaultMovementSpeed;
            noiseRange.enabled = false;
            anim.SetBool("isRunning", false);
        }

        #endregion End Player Movement

        if (Input.GetKeyDown(KeyCode.I))
        {
            CheckInventory();
        }
    }

    void CheckInventory()
    {
        if (anim.GetBool("OpenInventory") == false)
        {
            movementSpeed = stoppedSpeed;
            anim.SetBool("OpenInventory", true);
        }else{
            anim.SetBool("OpenInventory", false);
            movementSpeed = defaultMovementSpeed;
        }
    }
}
