using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorboardEvidence : MonoBehaviour
{
    public bool isTriggered;

    private void Start()
    {
        isTriggered = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isTriggered == false)
        {
            GetComponent<AudioSource>().Play();
            GetComponent<SphereCollider>().enabled = true;
            isTriggered = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isTriggered == true)
        {
            isTriggered = false;
            GetComponent<SphereCollider>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(other.gameObject.name + "heard a noise");
            other.gameObject.GetComponent<Enemy>().InvestigateLocation = gameObject;
            other.gameObject.BroadcastMessage("Investigate");
;        }
        //FindObjectOfType<Enemy>().BroadcastMessage("Attack"); // will not alert multiple enemies currently
    }

}
