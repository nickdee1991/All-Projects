using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogTrigger : MonoBehaviour
{
    private Animator anim;
    private LevelManager levelManager;
    public Transform BoxSpawnPosition;
    public Transform TriSpawnPosition;
    public Transform WheelSpawnPosition;
    private AudioManager aud;


    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
        aud = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Trigger Activated");
            aud.PlaySound("fogtrigger");
            anim.SetBool("Open", true);
            GameObject.Find("Player_HolderBox").transform.position = BoxSpawnPosition.transform.position;
            levelManager.lastCheckPointPosBox = BoxSpawnPosition.transform.position;
            levelManager.lastCheckPointPosTri = TriSpawnPosition.transform.position;
            levelManager.lastCheckPointPosWheel = WheelSpawnPosition.transform.position;
        }
    }
}
