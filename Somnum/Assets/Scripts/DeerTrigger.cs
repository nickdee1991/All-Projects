using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerTrigger : MonoBehaviour
{
    private AudioManager Aud;
    private bool hasTriggered;
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        Aud = FindObjectOfType<AudioManager>();
        levelManager = FindObjectOfType<LevelManager>();
        hasTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && hasTriggered == false)
        {
            Debug.Log("DeerTrigger");
            levelManager.StartCoroutine("ActivateSpeech");
            levelManager.speechText.text = "Good lord, what has done this.";
            Aud.PlaySound("violinLow");
        }
    }
}
