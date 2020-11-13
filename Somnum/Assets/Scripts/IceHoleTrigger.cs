using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceHoleTrigger : MonoBehaviour
{
    private AudioManager Aud;
    private LevelManager levelManager;
    private bool hasTriggered;

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
            Debug.Log("IceHoleTrigger");
            Aud.PlaySound("violinMed");
            levelManager.StartCoroutine("ActivateSpeech");
            levelManager.speechText.text = "This hole, too large for ice fishing.";
        }
    }
}
