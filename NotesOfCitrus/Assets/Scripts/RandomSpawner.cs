using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    private GameObject randomObject;
    public GameObject[] objectContainer;

    //public GameObject spawnPoint;
    public Transform[] spawnPoints;

    void Awake()
    {

        SpawnObjects();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnObjects();
        }
    }

    //Adds object to list of available spawn points
    public void SpawnObjects()
    {
        GameObject spawnedObjectContainer = new GameObject("SpawnedObjectContainer"); // create a container

        foreach (Transform spawnPoint in spawnPoints) // for every spawnpoint in the list *
        {
            int destPoint = (Random.Range(0, objectContainer.Length));
            GameObject spawnedObject = Instantiate(objectContainer[destPoint], spawnPoint); // * instantiate a random gameobject from the container
            spawnedObject.transform.parent = spawnedObjectContainer.transform; // parent all random spawned objects to new container for tidyness
        }    
    }
}
