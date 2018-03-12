﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(PlayerHealth))]
public class Throwable : MonoBehaviour {

    public float flyTime;
    public Collider childCollider;

    private bool flying = true;
    private Rigidbody rb;
    private Rigidbody rbEnemy;
    private float stopTime;
    private Transform anchor;
    public Camera deathCam;

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
            this.transform.LookAt(transform.position + rb.velocity);
        } else if (this.anchor != null)
        {
            this.transform.position = anchor.transform.position;
            this.transform.rotation = anchor.transform.rotation;
        }
        
    }
    // Throwable collider will become child on whatever it collides with and reset its transform to the parents relative position 
    //(ie the throwable will stick into the object it hits)
    private void OnCollisionEnter(Collision collision)
    {
        if (this.flying)
        {
            this.flying = false;
            this.transform.position = collision.contacts[0].point;
            this.childCollider.isTrigger = true;
            Debug.Log( "hit " + collision );

            GameObject anchor = new GameObject("Arrow_Anchor");
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor.transform;            

            Destroy(rb);
            collision.gameObject.SendMessage("arrowHit", SendMessageOptions.DontRequireReceiver);
        }

        //Check if throwable has collided with Enemy.Head, if so, disable kinematic, add gravity and apply force to head.
        if (collision.gameObject.tag.Equals("Head"))
        {
            rbEnemy = collision.gameObject.GetComponent<Rigidbody>();
            rbEnemy.isKinematic = false;
            rbEnemy.useGravity = true;
            rbEnemy.AddForce(transform.forward * 400);
            
            Debug.Log("hit " + name);
        }
    }




}
