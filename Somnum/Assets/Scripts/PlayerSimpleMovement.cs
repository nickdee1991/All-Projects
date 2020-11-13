using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleMovement : MonoBehaviour
{
    public int movementSpeed = 1;
    public int rotationSpeed = 5;
    public Animator rigAnim;
    public Animator anim;

    public void Update()
    {
        #region Player Movement
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            anim.SetBool("isWalking", true);
            rigAnim.SetBool("isWalking", true);
        }
        else{
            anim.SetBool("isWalking", false);
            rigAnim.SetBool("isWalking", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
            anim.SetBool("isWalking", true);
        }

        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Translate(-Vector3.right * movementSpeed * rotationSpeed * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Translate(Vector3.right * movementSpeed * rotationSpeed * Time.deltaTime);
        //}
        #endregion End Player Movement
    }
}
