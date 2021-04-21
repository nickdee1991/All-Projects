using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject door;
    private Animator triTriggerAnim;
    private AudioManager aud;

    public float timerWait;

    private void Start()
    {
        player = GameObject.Find("PlayerTri");
        triTriggerAnim = GetComponent<Animator>();
        aud = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(player))
        {
            Debug.Log("Door Activated");
            aud.PlaySound("dooropen");
            door.GetComponent<Animator>().SetBool("Open", true);
            triTriggerAnim.SetBool("Close", true);
            StartCoroutine("TriTimer");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(player))
        {
            triTriggerAnim.SetBool("Close", false);
        }
    }

    IEnumerator TriTimer()
    {
        yield return new WaitForSeconds(timerWait);
        door.GetComponent<Animator>().SetBool("Open", false);
    }
}
