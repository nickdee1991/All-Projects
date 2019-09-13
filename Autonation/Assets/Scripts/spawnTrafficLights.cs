using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTrafficLights : MonoBehaviour {

    public GameObject trafficLightPoint;
    public Transform lightSpawnpoint;


	// Use this for initialization
	void Start () {
        StartCoroutine(trafficLight());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator trafficLight()
    {
        //do this
        yield return new WaitForSeconds(Random.Range(0, 10));
            Instantiate(trafficLightPoint, lightSpawnpoint.position, Quaternion.identity);
        StartCoroutine(trafficLight());
    }
}
