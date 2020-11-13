using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTrigger : MonoBehaviour
{
    private AudioManager Aud;
    private LevelManager levelManager;
    public Light monsterLight;
    private bool InTrigger;
    private bool HasTriggered;
    public Animator Monster;

    private void Start()
    {
        Aud = FindObjectOfType<AudioManager>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && HasTriggered == false)
        {
            Debug.Log("MonsterTrigger");
            Aud.PlaySound("violinHigh");
            levelManager.StartCoroutine("ActivateSpeech");
            levelManager.speechText.text = "Dear god. What was that? I need to reach solid ground";
            monsterLight.enabled = true;
            InTrigger = true;
            HasTriggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InTrigger = false;
            HasTriggered = true;
        }
    }
    private void OnMouseOver()
    {
        if (InTrigger == true && HasTriggered == true)
        {
            Debug.Log("player spotted monster");
            Monster.SetBool("isMoving", true);
        }
    }
}
