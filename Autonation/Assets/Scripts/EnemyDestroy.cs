using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour {

    private GameObject enemy;

	// Use this for initialization
	void Start () {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
