using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHold : MonoBehaviour
{

    public float waitTime;
    private Transform camOriginalPos;
    private Transform camBasementPos;
    private Transform cabinCamPos;
    public InteractableManager IntMgr;
    public bool cameraIsHolding;


    private void Start()
    {
        cameraIsHolding = false;
        IntMgr = GameObject.FindGameObjectWithTag("InteractableManager").GetComponent<InteractableManager>();
        camOriginalPos = GameObject.Find("CameraOriginalPos").transform;
        camBasementPos = GameObject.Find("CameraBasementPos").transform;
        cabinCamPos = GameObject.Find("cabinCamPos").transform;
    }

    public IEnumerator CameraHolding()
    {
        cameraIsHolding = true;
        yield return new WaitForSeconds(waitTime);
        if (IntMgr.haslogInt == true)
        {
            this.gameObject.transform.position = camOriginalPos.transform.position;
            this.gameObject.transform.rotation = camOriginalPos.transform.rotation;
        }
        if (IntMgr.haslogInt == false && !IntMgr.inTherapist && !IntMgr.inBasement)
        {
            this.gameObject.transform.position = cabinCamPos.transform.position;
            this.gameObject.transform.rotation = cabinCamPos.transform.rotation;
        }

        if (IntMgr.inBasement)
        {
            this.gameObject.transform.position = camBasementPos.transform.position;
            this.gameObject.transform.rotation = camBasementPos.transform.rotation;
        }

        cameraIsHolding = false;
    }
}
