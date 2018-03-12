using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimController : MonoBehaviour {

    public Animator animator;
    public float speed = 2.0f;
    //NavMeshAgent agent;

   // const float locomotionAnimationSmoothTime = .1f;
    

	// Use this for initialization
	void Start ()
    {
        //agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        bool Throw = Input.GetMouseButton(0);

        float translationV = Input.GetAxis("Vertical") * speed;
        float translationH = Input.GetAxis("Horizontal") * speed;
        translationV *= Time.deltaTime;
        translationH *= Time.deltaTime;

        transform.Translate(0, 0, translationV);
        transform.Translate(0, 0, translationH);

        if (Throw)
        {
            animator.SetBool("throwBool", true);
        } else
        {
            animator.SetBool("throwBool", false);
        }



        if (translationV != 0 || translationH != 0)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        //float speedPercent = agent.velocity.magnitude / agent.speed;
        //animator.SetFloat("speedPercent" , speedPercent, .1f, Time.deltaTime);
    }
}
