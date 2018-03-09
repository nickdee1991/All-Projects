using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {

    public float flyTime;
    public Collider childCollider;

    private bool flying = true;
    private Rigidbody rb;
    private float stopTime;
    private Transform anchor;

    void Start ()
    {
        this.stopTime = Time.time + this.flyTime;
        rb = GetComponent<Rigidbody>();       
	}

    void Update()
    {
        if (this.stopTime <= Time.time && this.flying)
        {
            GameObject.Destroy(gameObject);
        }

        if (this.flying)
        {
           // this.transform.rotation = Quaternion.LookRotation(rb.velocity);
            this.transform.LookAt(transform.position + rb.velocity);
        } else if (this.anchor != null)
        {
            this.transform.position = anchor.transform.position;
            this.transform.rotation = anchor.transform.rotation;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.flying)
        {
            this.flying = false;
            this.transform.position = collision.contacts[0].point;
            this.childCollider.isTrigger = true;
            Debug.Log( "hit " + name );

            GameObject anchor = new GameObject("Arrow_Anchor");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor.transform;

            Destroy(rb);
            collision.gameObject.SendMessage("arrowHit", SendMessageOptions.DontRequireReceiver);
        }

        if (collision.gameObject.tag.Equals("Head") == true)
        {
            print("!!hit head!!");
        }


    }




}
