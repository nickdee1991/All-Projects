using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCollision : NetworkBehaviour
{

    public GameObject head;

    private Rigidbody rbHead;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    public LevelManager levelmanager;
    public GameObject sceneManager;

    public int Player1hit;

    public bool Player2Win = false;

    // Use this for initialization
    void Start()
    {
        Player1hit = 3;
        levelmanager = sceneManager.GetComponent<LevelManager>();
        //levelmanager.player2Win = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}