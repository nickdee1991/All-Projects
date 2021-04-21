using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformParent : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        player.transform.localScale = player.transform.localScale;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("on platform");
            //player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
            //player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("left platform");
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //player.transform.parent = null;
        }
    }

}
