using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {

    public GameObject Head;
    private Rigidbody rb;
    private bool enemyDecap = false;

	void Start ()
    {

	}

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.name == "Throwable")
        {
            //Head.AddComponent<Rigidbody>();
            //Instantiate<GameObject>(dead, transform.position, transform.rotation);
            Debug.Log("collided with " + name);
        }
        



    }

    // Update is called once per frame
    void Update () {
		
	}
}
