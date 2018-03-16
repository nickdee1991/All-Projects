using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Head2collision : MonoBehaviour {

    private Rigidbody rbHead;
    private PlayerMove Player2Move;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    public int Player2hit;
    public bool Player1Win = false;




    // Use this for initialization
    void Start()
    {
        Player2hit = 3;       
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Projectile"))
        {
            Player2hit --;

            if (Player2hit == 2)
            {
                Debug.Log("life1 enabled");
                life1.SetActive(true);
            }

            if (Player2hit == 1)
            {
                Debug.Log("life2 enabled");
                life2.SetActive(true);
            }

            Debug.Log("hit " + name + " Player2 Lives  = " + Player2hit);
            if (Player2hit <= 0)
            {
                life3.SetActive(true);
                Player1Win = true;
                rbHead = this.GetComponent<Rigidbody>();
                rbHead.isKinematic = false;
                rbHead.useGravity = true;
                rbHead.AddForce(transform.forward * 100);
                GameObject.FindGameObjectWithTag("Head2").transform.parent = null;
                //Player2Move.walkSpeed = 0;
            }                  
        }
    }
}