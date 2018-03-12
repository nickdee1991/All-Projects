using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int playerHealth = 100;
    public bool damageToPlayer;


	// Use this for initialization
	void Start ()
    {
        damageToPlayer = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (damageToPlayer == true)
        {
            playerHealth =  -10;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
