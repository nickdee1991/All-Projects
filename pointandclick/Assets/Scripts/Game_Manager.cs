using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

    public static Game_Manager ins;

    [HideInInspector]
    public Node currentNode;

    public CameraRig rig;

    void Awake()
    {
        ins = this;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && currentNode.GetComponent<Prop>() != null)
        {
            currentNode.GetComponent<Prop>().loc.Arrive();
        }
    }
}
