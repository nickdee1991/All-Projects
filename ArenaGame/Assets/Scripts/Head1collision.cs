using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerProjectile))]
public class Head1collision : MonoBehaviour {

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

    private void OnCollisionEnter(Collision collision)
    {
            if (collision.gameObject.tag.Equals("Projectile"))
            {
                Player1hit--;

                Debug.Log("PlayerCollision collisionDetected script active");

                if (Player1hit == 2)
                {
                    Debug.Log("life1 enabled");
                    life1.SetActive(true);
                }

                if (Player1hit == 1)
                {
                    Debug.Log("life2 enabled");
                    life2.SetActive(true);
                }

                Debug.Log("hit " + name + " Player1 Lives  = " + Player1hit);
                if (Player1hit <= 0)
                {
                    life3.SetActive(true);
                    Player2Win = true;
                    rbHead = this.GetComponent<Rigidbody>();
                    rbHead.isKinematic = false;
                    rbHead.useGravity = true;
                    rbHead.AddForce(transform.forward * 100);
                    GameObject.FindGameObjectWithTag("Head1").transform.parent = null;
                    levelmanager.player2Win = true;
                    GameObject.FindGameObjectWithTag("Player1").GetComponent<CharacterController>().enabled = false;
                }
            }      
    }

}