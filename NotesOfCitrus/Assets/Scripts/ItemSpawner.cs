using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private GameObject Item;

    List<int> randomInts = new List<int>();

    public Transform[] spawnPoints;
    public Transform currentSpawn;
    private int destPoint;
    private int newDestPoint;

    // Start is called before the first frame update
    void Start()
    {
        GenerateItem();
    }

    private void LateUpdate()
    {
        if (currentSpawn.transform.childCount > 1)
        {
            GenerateItem();
        }
    }

    //Adds prefab room to list of available spawn points
    public void GenerateItem()
    {
        Item = gameObject;

        //destPoint is assigned a random number = to the length of the spawnpoint array
        destPoint = (Random.Range(0, spawnPoints.Length));
        currentSpawn = spawnPoints[destPoint];

        //if this spawnpoint does not contain any other rooms, spawn here
        if (currentSpawn.transform.childCount == 0)
        {
            Debug.Log(destPoint);

            //Instantiate item in random location/rotation and parent on start
            Item.transform.parent = spawnPoints[destPoint];
            Item.transform.position = spawnPoints[destPoint].position;
            Item.transform.rotation = spawnPoints[destPoint].rotation;
        }
        else
        {
            //if there is already a room in this position - spawn somewhere else
            newDestPoint = (Random.Range(0, spawnPoints.Length));
            destPoint = newDestPoint;
            currentSpawn = spawnPoints[newDestPoint];
            Debug.Log(destPoint + " is currently occupied. Now using " + newDestPoint);
            Item.transform.parent = spawnPoints[newDestPoint];
            Item.transform.position = spawnPoints[newDestPoint].position;
            Item.transform.rotation = spawnPoints[newDestPoint].rotation;

        }
    }
}
