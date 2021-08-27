using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    public GameObject BreakageParticle;
    public GameObject ImpactParticle;

    public bool isBroken; // trigger for enemy noticing object

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isBroken)
        {
            Debug.Log(gameObject.name + "Hit Ground");
            //Destroy(gameObject,0.5f);
            GetComponent<AudioSource>().Play();
            GetComponent<SphereCollider>().enabled = true;
            BreakageParticle.GetComponent<ParticleSystem>().Play();
            ImpactParticle.GetComponent<ParticleSystem>().Play();
            isBroken = true;
            StartCoroutine("NoiseRadiusTimer");
        }
    }

    IEnumerator NoiseRadiusTimer() // disable noise radius after set time
    {
        yield return new WaitForSeconds(1);
        GetComponent<SphereCollider>().enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(other.gameObject.name + "heard a noise");
            other.gameObject.GetComponent<Enemy>().InvestigateLocation = gameObject;
            other.gameObject.BroadcastMessage("Investigate");
        }
    }
}
