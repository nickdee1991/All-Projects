using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour
{

    public Animator anim;
    private bool isColliding;
    public BoxCollider boxCol;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        isColliding = false;
        //Physics.IgnoreLayerCollision(GetComponent<Collider>(),);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Door")
        {
            Physics.IgnoreCollision(collision.collider, boxCol);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Door") && isColliding == false)
        {
            isColliding = true;
            anim.SetBool("isColliding", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false;
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Door"))
        {         
            anim.SetBool("isColliding", false);
        }
    }
}
