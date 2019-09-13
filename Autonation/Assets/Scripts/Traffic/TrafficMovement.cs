using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficMovement : MonoBehaviour {

    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}