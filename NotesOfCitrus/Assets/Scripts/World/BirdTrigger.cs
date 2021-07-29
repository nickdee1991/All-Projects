using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTrigger : MonoBehaviour
{
    private Animator anim;
    private AudioSource aud;
    public string triggerName;
    public float moveSpeed = 5;
    public float timeToDelete;
    private bool BirdMove;

    void Start()
    {
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        BirdMove = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            GetComponent<RandomSoundTrigger>().StartCoroutine("InteractingSound");
            anim.SetTrigger(triggerName);
            BirdMove = true;
            StartCoroutine("BirdTimer");
        }
    }

    private void Update()
    {
        if(BirdMove == true)
        {
            transform.position += -transform.right * moveSpeed * Time.deltaTime;
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        }
    }

    public IEnumerator BirdTimer()
    {
        yield return new WaitForSeconds(timeToDelete);
        Destroy(this.gameObject);
    }
}
