using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandReference : MonoBehaviour
{
    private MeshRenderer HandReferenceMesh;
    public Rigidbody rb;

    public SphereCollider corkTrigger;

    public bool collidingWithCorkTrigger;

    public Transform corkPosition;

    private void Start()
    {
        collidingWithCorkTrigger = true;
        HandReferenceMesh = GetComponentInChildren<MeshRenderer>();
        rb = GetComponentInParent<Rigidbody>();
    }

    public void ActivateReferenceMesh()
    {
        HandReferenceMesh.enabled = true;
    }

    public void DeactivateReferenceMesh()
    {
        HandReferenceMesh.enabled = false;
    }

    public void ActivateObject()
    {
        HandReferenceMesh.enabled = true;
    }

    public void DeselectObject()
    {
        rb.isKinematic = false;
        rb.detectCollisions = true;
        Debug.Log("object deactivated");

        if (collidingWithCorkTrigger)
        {
            Debug.Log("moving cork to set position");
            gameObject.transform.position = corkPosition.transform.position;
            gameObject.transform.rotation = corkPosition.transform.rotation;
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        other = corkTrigger;

        if (transform.parent.CompareTag("Cork"))
        {
            collidingWithCorkTrigger = true;
        }
        else
        {
            collidingWithCorkTrigger = false;
        }
    }
}
