using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour {

    public GameObject cam1;
    public GameObject cam2;

    bool toggleCam = false;

    // Use this for initialization
    void Start ()
    {
        cam1.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.C))
        { 
            toggleCam = !toggleCam;
            cam1.SetActive(toggleCam);
            cam2.SetActive(!toggleCam);
            
        }        
    }
}