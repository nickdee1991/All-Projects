using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icosphereRotation : MonoBehaviour {

    public float rotation = 50;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate (new Vector3(0, 0, Time.deltaTime * rotation));
	}
}
