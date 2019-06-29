﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingInteractable : MonoBehaviour {

    public bool openBlinds = false;

    public Animator blindsSwitch;

    //Opening blinds
    private void OnMouseDown()
    {
        if (openBlinds == false)
        {
            blindsSwitch.SetBool("OpenBlinds", true);
            openBlinds = true;
        } else
        if (openBlinds == true)
        {
            openBlinds = false;
            blindsSwitch.SetBool("OpenBlinds", false);
        }

    }


}
