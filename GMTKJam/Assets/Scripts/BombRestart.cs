using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRestart : MonoBehaviour
{
    public float radius;
    public float force;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponentInParent<Rigidbody>();

                if (rb != null)
                {
                    //rb = hit.GetComponentInParent<Rigidbody>();
                    
                    rb.isKinematic = false;
                    rb.AddExplosionForce(force, explosionPos, radius, 3.0F);
                    //Debug.Log("Boom");
                }


            }
            GetComponent<AudioSource>().Play();
            GetComponent<ParticleSystem>().Play();
            other.gameObject.GetComponentInParent<Rigidbody>().AddExplosionForce(force, explosionPos, radius, 3.0f);
            FindObjectOfType<GameManager>().StartCoroutine("RestartLevel");
            FindObjectOfType<GameManager>().hasDied = true;
            //Debug.Log("Player Exploded - Restarting Level");

        }
    }
}
