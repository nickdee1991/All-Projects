using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    private GameObject player;
    public bool haslogInt; //collecting log outside
    public bool hasHammerInt; //hammer to barricade
    public bool secureDoor; // barracading door shut
    public bool breakChair; // breaking down chair for fire
    public bool startFire; // starting fire with salvaged wood
    public bool pryHatch; // prying the basement hatch
    public bool inBasement; // is the player in the basement
    public bool inTherapist; // is the player in the basement
    public bool itemCollected; // collected basement item
    public bool itemDestroyed; // destroyed basement item
    public bool itemUsed; // used basement item on enemy

    private GameObject Item;

    public Transform[] spawnPoints;
    private int destPoint = 0;

    public enum Ending {Start, NoItem, Item, ItemDestroyed, ItemBoss};
    public Ending chosenEnding;
    //Start = the default state for when no conditions are met for any ending
    //NoItem = ran through level 
    //Item = Obtained item and brought to end 
    //ItemDestroyed = Item collected and thrown into fire 
    //ItemBoss = Item shown to boss



    void Start()
    {
        chosenEnding = Ending.Start;
        Item = GameObject.FindGameObjectWithTag("Item");
        haslogInt = false;
        secureDoor = false;
        breakChair = false;
        startFire = false;
        pryHatch = false;
        inBasement = false;
        player = GameObject.FindGameObjectWithTag("Player");

        //destPoint is assigned a random number = to the length of the spawnpoint array
        destPoint = (Random.Range(0, spawnPoints.Length));

        //Instantiate item in random location on start
        Item.transform.position = spawnPoints[destPoint].position;
    }
}
