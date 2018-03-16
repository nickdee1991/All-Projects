using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head1collision : MonoBehaviour {

    private Rigidbody rbEnemy;

    public LevelManager levelManager;

    public int Player1hit;
    public bool Player2Win = false;


    // Use this for initialization
    void Start()
    {
        Player1hit = 0;
        levelManager = GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Projectile"))
        {
            Player1hit++;
            Debug.Log("hit " + name + "player1hit " + Player1hit);
            if (Player1hit >= 3)
            {
                rbEnemy = this.GetComponent<Rigidbody>();
                rbEnemy.isKinematic = false;
                rbEnemy.useGravity = true;
                rbEnemy.AddForce(transform.forward * 100);
                GameObject.FindGameObjectWithTag("Head1").transform.parent = null;
                Player2Win = true;
            }
        }
    }
}