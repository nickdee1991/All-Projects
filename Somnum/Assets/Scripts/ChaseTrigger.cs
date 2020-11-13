using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTrigger : MonoBehaviour
{
    private LevelManager levelManager;
    public AudioSource Aud;
    public GameObject DeathWall;
    public GameObject Player;
    public Light monsterLight;
    public bool DeathWallMoving;
    private bool hasTriggered;

    public float moveSpeed;

    private void Start()
    {
        hasTriggered = false;
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        if (DeathWallMoving)
        {
            DeathWall.transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && hasTriggered == false)
        {
            Debug.Log("ChaseTrigger");
            levelManager.StartCoroutine("ActivateSpeech");
            levelManager.speechText.text = "It's following me";
            Player.GetComponent<PlayerSimpleMovement>().movementSpeed = 6;
            Player.GetComponent<Animator>();
            DeathWall.GetComponent<BoxCollider>().enabled = true;
            monsterLight.enabled = true;
            //DeathWall.GetComponent<AudioSource>().Play(); this script is attached to all the boundaries too. So will need to add check to only turn on the death wall aud
            DeathWallMoving = true;
            hasTriggered = true;
            Aud.Play();
        }
    }
}
