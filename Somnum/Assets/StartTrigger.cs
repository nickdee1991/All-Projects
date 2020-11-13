using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
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
            Debug.Log("StartTrigger");
            levelManager.StartCoroutine("ActivateSpeech");
            levelManager.speechText.text = "Good God, I can't have travelled this far in my slumber. I'll need to retrace my steps home";
        }
    }
}
