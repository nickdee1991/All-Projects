using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beartrap : MonoBehaviour
{
    private AudioManager aud;

    private ParticleSystem blood;

    private bool isTrapped;
    [SerializeField]
    private float trappedTime = 3f;

    private PlayerSimpleMovement playerSpeed;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerSpeed = FindObjectOfType<PlayerSimpleMovement>();
        aud = FindObjectOfType<AudioManager>();
        blood = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isTrapped)
        {
            StartCoroutine("PlayerTrapped");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isTrapped)
        {
            isTrapped = false;
        }
    }

    public IEnumerator PlayerTrapped()
    {
        Debug.Log("Player trapped");
        // stop player speed
        playerSpeed.isTrapped = true;
        playerSpeed.movementSpeed = playerSpeed.stoppedSpeed;
        //alert nearby enemies
        FindObjectOfType<Enemy>().BroadcastMessage("Attack"); // will not alert multiple enemies currently
        //playsound
        aud.PlaySound("Beartrap");
        //play particle effect
        blood.Play();

        yield return new WaitForSeconds(trappedTime);
        playerSpeed.isTrapped = false;
        playerSpeed.movementSpeed = playerSpeed.defaultMovementSpeed; // reset movement speed once timer reaches 0
        Debug.Log("Player released");
    }
}
