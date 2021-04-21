using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour
{

    public Animator anim;
    private bool isColliding;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        isColliding = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") && isColliding == false)
        {
            isColliding = false;
            anim.SetBool("isColliding", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            isColliding = true;
            anim.SetBool("isColliding", false);
        }
    }
}
