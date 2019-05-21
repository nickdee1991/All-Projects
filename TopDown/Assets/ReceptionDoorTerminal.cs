using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionDoorTerminal : MonoBehaviour {

    public GameObject door;
    public GameObject fuseTerminal;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            if (Input.GetKey(KeyCode.E) && fuseTerminal.GetComponent<ReceptionDoorKey>().fusePickedUp == true)
            {
                Debug.Log("Fuse placed, door opening");
                door.transform.Translate(Vector3.up * Time.deltaTime * 7.5f);
            }
        }
    }
}
