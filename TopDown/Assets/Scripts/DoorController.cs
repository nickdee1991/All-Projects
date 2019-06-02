using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public float openingSpeed = 10;
    public bool isOpening;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isOpening == true)
        {
            transform.Translate(Vector3.up * Time.deltaTime * openingSpeed);
        }
        if (transform.position.y > 7f)
        {
            isOpening = false;
        }
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            isOpening = true;
        }
    }
}
