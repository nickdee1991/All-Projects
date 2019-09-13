using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeCollected : MonoBehaviour {

    public bool tapeCollected = false;

    void OnMouseDown()
    {
        tapeCollected = true;
        this.gameObject.SetActive(false);
    }
}
