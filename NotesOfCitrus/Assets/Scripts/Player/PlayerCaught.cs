using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCaught : MonoBehaviour
{
    private GameManager GM;
    public bool Captured;
    public GameObject enemyCapturedBy;
    private AudioManager aud;

    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        aud = FindObjectOfType<AudioManager>();
        Captured = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Father") || collision.gameObject.CompareTag("Mother") || collision.gameObject.CompareTag("Junior") && Captured == false)
        {
            enemyCapturedBy = collision.gameObject;
            enemyCapturedBy.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine("EnemyColliderCooldown");
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
            if(other.gameObject.GetComponentInChildren<PatrolRandom>().enabled == false)
            {
                other.gameObject.GetComponentInChildren<PatrolRandom>().enabled = true;
            }
            other.gameObject.GetComponent<Enemy>().Attack();
        }
    }

    IEnumerator EnemyColliderCooldown()
    {
        yield return new WaitForSeconds(4);
        enemyCapturedBy.GetComponent<BoxCollider>().enabled = true;
    }
}
