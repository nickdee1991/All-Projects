using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryController : MonoBehaviour
{
    public float radius;
    public float force;

    public ParticleSystem explosion;
    private Animator anim;

    private bool hasShot;

    private void Start()
    {
        anim = GetComponent<Animator>();
        hasShot = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasShot)
        {
            hasShot = true;
            anim.SetBool("move", true);
            GetComponent<AudioSource>().Play();
            explosion.Play();
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponentInParent<Rigidbody>();

                if (rb != null && rb.transform.gameObject.tag !=("Player"))
                {
                    rb.isKinematic = false;
                    rb.AddExplosionForce(force, explosionPos, radius, 3.0F);
                }
            }

            //other.gameObject.GetComponentInParent<Rigidbody>().AddExplosionForce(force, explosionPos, radius, 3.0f);
        }
    }
}
