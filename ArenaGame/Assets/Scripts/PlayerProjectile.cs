using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {

    private float throwableInScene = 0;
    public float throwableAmmo;
    public Rigidbody spear;
    public Transform projectilePoint;
    public float throwForce = 2500;

    public GameObject throwable1;
    public GameObject throwable2;
    public GameObject throwable3;


    public float Cooldown = 1;
    public float cooldownTimer;

    // Use this for initialization
    void Start ()
    {
        throwableAmmo = 3;
    }
	
	void Update ()
    {
        if (throwableAmmo < 0)
        {
            throwableAmmo = 0;
        }

        if (Input.GetButtonDown("Fire1") && cooldownTimer == 0)
        {
            //Throwing = State.Throwing;
            if (throwableAmmo > 0)
            {
                Throw();
            }
            Debug.Log("Cooldown started");
            cooldownTimer = Cooldown;           
        }

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (cooldownTimer < 0)
        {
            cooldownTimer = 0;
        }
	}

    void Throw ()
    {
        Rigidbody spearInstance;
        spearInstance = Instantiate(spear, projectilePoint.transform.position, projectilePoint.transform.rotation) as Rigidbody;      
        spearInstance.AddForce(projectilePoint.forward * throwForce);
        throwableInScene ++;
        throwableAmmo--;

        if (throwableAmmo == 2)
        {
            throwable3.SetActive(false);
        }
        if (throwableAmmo == 1)
        {
            throwable2.SetActive(false);
        }
        if (throwableAmmo == 0)
        {
            throwable1.SetActive(false);
        }

        {
            if (throwableInScene >= 20)
            {
                Destroy(GameObject.Find("Throwable(Clone)"));
            }
        }
    }
}
