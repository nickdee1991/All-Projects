using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleMovement : MonoBehaviour
{
    public float defaultMovementSpeed;

    public float movementSpeed;
    public float stoppedSpeed = 0;
    public float sprintSpeed;
    public Animator anim;
    public Animator playerGfxAnim;

    public bool isTrapped;

    public SphereCollider noiseRange;

    private void Start()
    {
        movementSpeed = defaultMovementSpeed;
        //anim = GetComponentInChildren<Animator>();
        noiseRange.enabled = false;
    }

    public void Update()
    {
        #region Player Movement
        if (!isTrapped)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                anim.SetBool("isMoving", true);
                playerGfxAnim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
                playerGfxAnim.SetBool("isWalking", false);
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

            if (Input.GetKey(KeyCode.C))
            {
                playerGfxAnim.SetBool("isCrouching", true);
            }
            else
            {
                playerGfxAnim.SetBool("isCrouching", false);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                movementSpeed = sprintSpeed;
                anim.SetBool("isRunning", true);
                playerGfxAnim.SetBool("isRunning", true);
                noiseRange.enabled = true;
            }
            else
            {
                movementSpeed = defaultMovementSpeed;
                noiseRange.enabled = false;
                anim.SetBool("isRunning", false);
                playerGfxAnim.SetBool("isRunning", false);
            }
        }

        #endregion End Player Movement

        if (isTrapped)
        {
            Trapped();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            CheckInventory();
        }
    }

    void Trapped()
    {
        anim.SetBool("isMoving", false);
        anim.SetBool("isRunning", false);
        //TEST - maybe play sound and new animation
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
