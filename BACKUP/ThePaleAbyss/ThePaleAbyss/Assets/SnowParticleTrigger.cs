using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowParticleTrigger : MonoBehaviour
{

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<ParticleSystem>().Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(player))
        {
            //gameObject.SetActive(true);
            GetComponent<ParticleSystem>().Play();
            Debug.Log(gameObject + ("isTriggered, enabling particles"));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(player))
        {
            //gameObject.SetActive(false);
            GetComponent<ParticleSystem>().Stop();
            Debug.Log(gameObject + ("isTriggered, disabling particles"));
        }
    }
}
