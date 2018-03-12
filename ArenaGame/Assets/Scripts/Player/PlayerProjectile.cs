using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {

    private float spearsThrown = 0f;
    public Rigidbody spear;
    public Transform projectilePoint;
    public float throwForce = 2500;
    //private Camera deathCam;

    // Use this for initialization
    void Start ()
    {
        //deathCam = Camera.tag("DeathCamera");
    }
	
	void Update ()
    {
		if (Input.GetButtonDown("Fire1"))
        {
            //Throwing = State.Throwing;
            Throw();           
        }
	}

    void Throw ()
    {
        Rigidbody spearInstance;
        spearInstance = Instantiate(spear, projectilePoint.transform.position, projectilePoint.transform.rotation) as Rigidbody;      
        spearInstance.AddForce(projectilePoint.forward * throwForce);
        spearsThrown ++;
        {
            if (spearsThrown >= 20)
            {
                Destroy(GameObject.Find("Throwable(Clone)"));
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitEnemy();
    }

    void HitEnemy()
    {

        if (gameObject.name == "Throwable")
        {
            // rb.isKinematic = false;
           // deathCam = GameObject.FindGameObjectWithTag("DeathCamera");
            Debug.Log("collided with " + name);
        }
    }
}
