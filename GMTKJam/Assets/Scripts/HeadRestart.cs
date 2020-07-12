using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRestart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().StartCoroutine("RestartLevel");
            FindObjectOfType<GameManager>().hasDied = true;
            GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Restarting Level");
        }
    }
}
