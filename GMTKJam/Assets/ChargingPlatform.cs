using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingPlatform : MonoBehaviour
{
    public TMPro.TextMeshProUGUI chargingText;
    private AudioManager aud;
    private GameManager gm;

    private void Start()
    {
        aud = FindObjectOfType<AudioManager>();
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //chargingText.enabled = true;
            chargingText.text = "Disconnected";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //chargingText.enabled = true;
            chargingText.text = "Connected \n Charging: 40%";
            if (FindObjectOfType<UpgradePlatform>().levelComplete == true)
            {
               Debug.Log("level complete");
               gm.StartCoroutine("LevelComplete");
            }
            
        }
    }

}
