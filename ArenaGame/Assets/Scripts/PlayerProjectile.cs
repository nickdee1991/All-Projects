using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {

    //private enum State { Idle, Throwing }
    private float spearsThrown = 0f;
    public Rigidbody spear;
    public Transform projectilePoint;
    public float throwForce = 2500;

    [SerializeField]
    private Camera cam;


    //State Throwing;
    //State Idle;

    // Use this for initialization
    void Start ()
    {
        //Idle = State.Idle;
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
        cam = GetComponent<Camera>();
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
}
