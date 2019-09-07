using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour {

    private GameObject enemy;

	// Use this for initialization
	void Start () {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(enemy))
        {
            Destroy(other.gameObject);
        }
    }
}
