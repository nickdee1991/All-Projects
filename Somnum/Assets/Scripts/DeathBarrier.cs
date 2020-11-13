using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBarrier : MonoBehaviour
{
    private AudioManager Aud;
    private PlayerSimpleMovement playerMove;
    public MeshRenderer deathScreen;
    public float deathDelay;

    private void Start()
    {
        Aud = FindObjectOfType<AudioManager>();
        playerMove = FindObjectOfType<PlayerSimpleMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {           
            StartCoroutine("DeathBarrierTimer");
            Debug.Log("player has died");
        }
    }

    IEnumerator DeathBarrierTimer() // event for when players dies. darken screen, play sound, then restart game
    {
        playerMove.movementSpeed = 0; // player footsteps will still play because they are tied to input and not speed
        Aud.PlaySound("IceCrackDeath");
        deathScreen.enabled = true;
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
