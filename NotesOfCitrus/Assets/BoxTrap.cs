using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrap : MonoBehaviour
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
        if (other.gameObject.CompareTag("Enemy") && !isTrapped)
        {
            StartCoroutine("IncapacitateEnemy");
        }
    }

    public IEnumerator IncapacitateEnemy()
    {
        Debug.Log("Enemy Incapacitated");
        // stop enemy speed

        //enable ragdoll

        //playsound
        aud.PlaySound("Beartrap");
        //play particle effect
        blood.Play();

        yield return new WaitForSeconds(trappedTime);
        isTrapped = false;
        //resume enemy speed
        //start animator back up
        Debug.Log("enemy released");
    }
}
