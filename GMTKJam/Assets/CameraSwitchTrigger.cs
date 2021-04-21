using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    //private GameObject mainCam;
    public GameObject[] disabledTriggerCams;
    public GameObject camsToDisable;
    public GameObject triggerCam;

    private void Start()
    {
        camsToDisable = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //mainCam.SetActive(false);
            foreach (var camsToDisable in disabledTriggerCams)
            {
                //GameObject newCamera = GameObject.FindGameObjectWithTag("MainCamera"); //gameObject.CompareTag("MainCamera");
                camsToDisable.SetActive(false);
                triggerCam.SetActive(true);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //mainCam.SetActive(true);
            //disabledTriggerCams.SetActive(false);
            //triggerCam.SetActive(false);

            foreach (var camsToDisable in disabledTriggerCams)
            {
                //GameObject newCamera = GameObject.FindGameObjectWithTag("MainCamera"); //gameObject.CompareTag("MainCamera");
                camsToDisable.SetActive(false);
                triggerCam.SetActive(true);
            }
        }
    }
}
