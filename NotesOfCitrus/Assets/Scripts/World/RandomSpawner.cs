using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    private GameObject SpawnedObjectContainer;
    public GameObject[] objectContainer;

    //public GameObject spawnPoint;
    public Transform[] spawnPoints;

    void Awake()
    {
        SpawnObjects();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnObjects();
        }
    }

    //Adds object to list of available spawn points
    public void SpawnObjects()
    {
        foreach (Transform spawnPoint in spawnPoints) // for every spawnpoint in the list *
        {
            GameObject spawnedObjectContainer = new GameObject("SpawnedObjectContainer"); // create a container

            int destPoint = (Random.Range(0, objectContainer.Length));
            GameObject spawnedObject = Instantiate(objectContainer[destPoint], spawnPoint); // * instantiate a random gameobject from the container

            spawnedObject.transform.parent = spawnedObjectContainer.transform; // parent all random spawned objects to new container for tidyness

            SpawnedObjectContainer = spawnedObjectContainer;
        }
    }

    public void RespawnObjects()
    {
        //Destroy(SpawnedObjectContainer);
        SpawnedObjectContainer.SetActive(false);
        SpawnObjects();
    }
}
