using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlatform : MonoBehaviour
{
    public TMPro.TextMeshProUGUI upgradeText;
    private AudioManager aud;
    public bool levelComplete;

    private void Start()
    {
        levelComplete = false;
        aud = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter
        (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //chargingText.enabled = true;
            aud.PlaySound("TerminalActivate");
            upgradeText.text = "Auto-Roam Upgrade Enabled. Place on charge to complete";
            levelComplete = true;
        }
    }

}
