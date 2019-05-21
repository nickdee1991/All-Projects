using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour {

    //private Animator anim;

	// Use this for initialization
	void Start () {
        //anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            this.transform.Translate(Vector3.up * Time.deltaTime * GetComponent<DoorController>().openingSpeed);
            //anim.SetBool("DoorOpen", true);
            //anim.SetBool("DoorClose", false);
            if (transform.position.y > 7f)
            {
                GetComponent<DoorController>().isOpening = false;
            }
        }       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            this.transform.Translate(Vector3.down * Time.deltaTime * GetComponent<DoorController>().openingSpeed);
            if (transform.position.y < 7f)
            {
                GetComponent<DoorController>().isOpening = true;
            }
            //anim.SetBool("DoorOpen", false);
            //anim.SetBool("DoorClose", true);
        }
    }
}
