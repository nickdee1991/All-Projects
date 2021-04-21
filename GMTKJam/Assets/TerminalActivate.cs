using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalActivate : MonoBehaviour
{
    public TMPro.TextMeshProUGUI terminalText;
    private AudioManager aud;

    private void Start()
    {
        aud = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            terminalText.enabled = true;
            terminalText.text = "Place GloryBot on Upgrade Platform";
        }
    }
}
