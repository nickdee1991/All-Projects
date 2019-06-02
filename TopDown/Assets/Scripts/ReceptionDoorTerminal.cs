using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionDoorTerminal : MonoBehaviour {

    public GameObject door;
    public GameObject fuseTerminal;
    public GameObject floatingText;


    // Use this for initialization
    private void OnMouseEnter()
    {
        floatingText.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        floatingText.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (fuseTerminal.GetComponent<ReceptionDoorKey>().fusePickedUp == true)
        {
            Debug.Log("Fuse placed, door opening");
            door.transform.Translate(Vector3.up * Time.deltaTime * 14f);
        }
    }
}
