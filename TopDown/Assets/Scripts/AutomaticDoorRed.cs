using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoorRed : MonoBehaviour {

    private GameObject player;
    public GameObject redAccessPanel;

    private Vector3 startPos;
    private Vector3 endPos;

    private bool doorOpen = false;
    private bool doorPassive = true;

    private float distance = 2.5f;
    private float lerpTime = .2f;
    private float currentLerpTime = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPos = transform.position;
        endPos = transform.position + Vector3.up * distance;
    }
    private void Update()
    {
        if(doorOpen == true)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float Perc = currentLerpTime / lerpTime;
            transform.position = Vector3.Lerp(startPos, endPos, Perc);
            doorPassive = false;
        }
        if (doorOpen == false && doorPassive == false)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float Perc = currentLerpTime / lerpTime;
            transform.position = Vector3.Lerp(endPos, startPos, Perc);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            doorOpen = true;
            OpenDoor();
        }
        if (other.gameObject.CompareTag("Player") && player.GetComponent<Player>().hasKeycard == true && redAccessPanel.GetComponent<RedAccessPanel>().unlocked == true)
        {
            Debug.Log("Door Opening");
            doorOpen = true;
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            doorOpen = false;
            CloseDoor();
        }
        if (other.gameObject.CompareTag("Player") && player.GetComponent<Player>().hasKeycard == true && redAccessPanel.GetComponent<RedAccessPanel>().unlocked == true)
        {
            doorOpen = false;
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        currentLerpTime = 0;
        //Debug.Log("Door Opening");
        float Perc = currentLerpTime / lerpTime;
        transform.position = Vector3.Lerp(startPos, endPos, Perc);
    }

    private void CloseDoor()
    {
        currentLerpTime = 0;
        //Debug.Log("Door Closing");
        float Perc = currentLerpTime / lerpTime;
        transform.position = Vector3.Lerp(endPos, startPos, Perc);
    }
}
