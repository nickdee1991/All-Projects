using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionDoorKey : MonoBehaviour {

    public bool fusePickedUp = false;
    public GameObject floatingText;

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
        fusePickedUp = true;
        Debug.Log("Reception Door Fuse Picked Up");
        gameObject.SetActive(false);
    }
}
