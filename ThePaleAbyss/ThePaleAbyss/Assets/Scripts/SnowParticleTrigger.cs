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
        GetComponent<ParticleSystem>().Play();
        GetComponent<ParticleSystemRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(player))
        {
            //gameObject.SetActive(true);
            //GetComponent<ParticleSystem>().Play();
            GetComponent<ParticleSystemRenderer>().enabled = true;
            Debug.Log(gameObject + ("isTriggered, enabling particles"));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(player))
        {
            //gameObject.SetActive(false);
            //GetComponent<ParticleSystem>().Stop();
            GetComponent<ParticleSystemRenderer>().enabled = false;
            Debug.Log(gameObject + ("isTriggered, disabling particles"));
        }
    }
}
