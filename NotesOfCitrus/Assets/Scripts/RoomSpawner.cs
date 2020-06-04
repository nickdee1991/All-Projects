using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    private GameObject Room;
    //public GameObject playerTemplate;
    private static int usedSpawnPoint;

    List<int> randomInts = new List<int>();

    public Transform[] spawnPoints;
    public Transform currentSpawn;
    private int destPoint;
    private int newDestPoint;

    // Start is called before the first frame update
    void Start()
    {
        // pick a random color
        //Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);

        // apply it on designated object's material
        //playerTemplate.GetComponent<Renderer>().material.color = newColor;

        GenerateRoom();
    }

    private void LateUpdate()
    {
        if (currentSpawn.transform.childCount > 1)
        {
            GenerateRoom();
        }
    }

    //Adds prefab room to list of available spawn points
    public void GenerateRoom()
    {

        Room = gameObject;

        //destPoint is assigned a random number = to the length of the spawnpoint array
        destPoint = (Random.Range(0, spawnPoints.Length));
        currentSpawn = spawnPoints[destPoint];

        //if this spawnpoint does not contain any other rooms, spawn here
        if (currentSpawn.transform.childCount == 0)
        {
            //Debug.Log(destPoint);

            //Instantiate item in random location/rotation and parent on start
            Room.transform.parent = spawnPoints[destPoint];
            Room.transform.position = spawnPoints[destPoint].position;
            Room.transform.rotation = spawnPoints[destPoint].rotation;
        }else{
            //if there is already a room in this position - spawn somewhere else
            newDestPoint = (Random.Range(0, spawnPoints.Length ));
            destPoint = newDestPoint;
            currentSpawn = spawnPoints[newDestPoint];
            //Debug.Log(destPoint + " is currently occupied. Now using " + newDestPoint);
            Room.transform.parent = spawnPoints[newDestPoint];
            Room.transform.position = spawnPoints[newDestPoint].position;
            Room.transform.rotation = spawnPoints[newDestPoint].rotation;

        }
    }
}
