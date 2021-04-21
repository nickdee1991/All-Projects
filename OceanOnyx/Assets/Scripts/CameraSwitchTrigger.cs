using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    public GameObject[] disabledTriggerCams;
    public GameObject[] camsToDisable;
    public GameObject triggerCam;

    private void Start()
    {
        camsToDisable = GameObject.FindGameObjectsWithTag("MainCamera");
        disabledTriggerCams = GameObject.FindGameObjectsWithTag("MainCamera");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("player triggered camera switch");
            foreach (var camsToDisable in disabledTriggerCams)
            {
                camsToDisable.SetActive(false);
                triggerCam.SetActive(true);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (var camsToDisable in disabledTriggerCams)
            {
                camsToDisable.SetActive(false);
                triggerCam.SetActive(true);
            }
        }
    }
}
