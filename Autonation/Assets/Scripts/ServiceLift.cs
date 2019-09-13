using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLift : MonoBehaviour {

    public GameObject lift;
    public bool liftUp;
    public float riseSpeed = 2;
    public Animator animator;

    // Use this for initialization
    void Start ()
    {
        liftUp = false;
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && liftUp == false)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("Lift Up");
                lift.transform.position += transform.up * riseSpeed * Time.deltaTime;
                animator.SetBool("ServiceLiftSwitch", true);

            }
        }
    }
}
