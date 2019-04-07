using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficDespawn : MonoBehaviour {

    public GameObject traffic;

    public Transform[] trafficSpawns;

    private int trafficSpawnSpot;

    private void Awake()
    {
        trafficSpawnSpot = Random.Range(0, trafficSpawns.Length);
    }


    private void Update()
    {
        if (trafficSpawnSpot == -1)
        {
            trafficSpawnSpot = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Traffic"))
        {
            Destroy(other.gameObject);
            trafficSpawnSpot = Random.Range(0, trafficSpawns.Length);
            Instantiate(traffic, trafficSpawns[trafficSpawnSpot].position, transform.rotation);
        }
    }
}
