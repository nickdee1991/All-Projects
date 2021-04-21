using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 3.5f;
    public int rotationSpeed = 35;

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void Update()
    {
        #region Player Movement
        {
            if (Input.anyKey)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                    anim.SetBool("forward", true);
                    anim.SetBool("back", false);
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
                    anim.SetBool("back", true);
                    anim.SetBool("forward", false);
                }
            }
            else
            {
                anim.SetBool("forward", false);
                anim.SetBool("back", false);
            }
            #endregion End Player Movement
        }
    }
}
