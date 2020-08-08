using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTraffic : MonoBehaviour
{
    public GameObject[] traffic;

    public Transform[] trafficSpawns;

    private int trafficSpawnSpot;
    private int trafficSpawnRandom;

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
        if (other.gameObject.CompareTag("Ship"))
        {
            trafficSpawnSpot = Random.Range(0, trafficSpawns.Length);
            other.gameObject.transform.position = trafficSpawns[trafficSpawnSpot].position;
            other.gameObject.transform.rotation = trafficSpawns[trafficSpawnSpot].rotation;
        }
    }
}
