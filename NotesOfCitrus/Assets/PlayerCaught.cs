using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCaught : MonoBehaviour
{
    private GameManager GM;
    public bool Captured;

    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        Captured = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Father") || collision.gameObject.CompareTag("Mother") || collision.gameObject.CompareTag("Junior") && Captured == false)
        {
            Debug.Log("GET. CAPTURED.");
            GM.Captured();
            Captured = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Father") || other.gameObject.CompareTag("Mother") || other.gameObject.CompareTag("Junior") && Captured == false)
        {
            Debug.Log(other.gameObject.name + "alerted, moving to your position");
            other.gameObject.GetComponent<Enemy>().MoveToTarget();
        }
    }
}
