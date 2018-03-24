using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
    public float walkSpeed = 10.0f;
    public float jumpHeight = 5.0f;
    public float gravity = 30.0f;
    private Vector3 moveDir = Vector3.zero;

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= walkSpeed;
            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpHeight;
            }
        }
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);

    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Debug.Log("is local player");
    }
}

