using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferencePoint : MonoBehaviour
{
    void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
