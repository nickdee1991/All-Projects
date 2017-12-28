using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public abstract class Node : MonoBehaviour {

    public Transform cameraPosition;
    public List<Node> reachableNodes = new List<Node>();

    [HideInInspector]
    public Collider col;

    // Use this for initialization
    void Start() {
        col = GetComponent<Collider>();
    }

    void OnMouseDown() {
        Arrive();
    }

    public void Arrive()
        {
        //leave existing currentNode
        if (Game_Manager.ins.currentNode != null)
        Game_Manager.ins.currentNode.Leave();

        //set currentNode
        Game_Manager.ins.currentNode = this;

        //move the camera
        Game_Manager.ins.rig.AlignTo(cameraPosition);

        //turn off our own collider
        if (col != null)
            col.enabled = false;

        //turn on all reachable nodes colliders
        foreach (Node node in reachableNodes)
        {
        if (node.col != null)
        {
        node.col.enabled = true;
            }
       
        }
    }
    public void Leave()
    {
        //turn off all reachable nodes colliders
        foreach (Node node in reachableNodes)
        {
            if (node.col != null)
            {
                node.col.enabled = false;
            }

        }
    }
}
