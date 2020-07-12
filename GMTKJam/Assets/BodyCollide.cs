using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollide : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("body collided");
            GetComponent<AudioSource>().Play();
        }
    }
}
