using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {

    private enum State { Idle, Throwing }
    private float spearsThrown = 0f;


    public GameObject spear;
    public Transform projectilePoint;

    State Throwing;
    State Idle;

    // Use this for initialization
    void Start ()
    {
        Idle = State.Idle;
        projectilePoint = transform.Find("ProjectilePoint");
        spear = Resources.Load("Throwable") as GameObject;
        print(Idle);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetMouseButtonDown(0))
        {
            Throw();
            Throwing = State.Throwing;
            print(Throwing);
        }
        else
        {
            print(Idle);
        }
	}

    void Throw ()
    {
        GameObject projectile = Instantiate(spear) as GameObject;
        projectile.transform.position = transform.position + projectilePoint.transform.forward * 4;
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = projectilePoint.transform.forward * 25;
        spearsThrown ++;
        print(spearsThrown);
        {
            if (spearsThrown >= 10)
            {
                Destroy(GameObject.Find("Throwable(Clone)"));
            }
        }
    }
}
