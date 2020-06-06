using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    public GameManager GM;
    private Rigidbody rb;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GM.EndKey == true)
        {
            rb.useGravity = true;
            Debug.Log("Found key, door unlocked");
            SceneManager.LoadScene("Outro");
        }
    }
}
