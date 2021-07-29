using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelTrigger : MonoBehaviour
{
    private GameManager gm;
    private AudioManager aud;
    public bool endLevelTrigger;
    public bool playSound;
    private float audioTimer;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        aud = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && endLevelTrigger)
        {
            StartCoroutine("AudioTimer");
        }
    }

    private IEnumerator AudioTimer()
    {
        if (playSound)
        {
            aud.PlaySound("EndLevelTrigger");
        }
        yield return new WaitForSeconds(audioTimer);
        gm.LevelComplete();
    }
}
