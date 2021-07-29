using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beartrap : MonoBehaviour
{
    private bool isTrapped;
    private float trappedTime = 3;

    private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isTrapped)
        {
            StartCoroutine("PlayerTrapped");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(player) && isTrapped)
        {
            isTrapped = false;
        }
    }

    public IEnumerator PlayerTrapped()
    {
        Debug.Log("Player trapped");
        // stop player speed
        //alert nearby enemies
        //playsound
        //play particle effect
        yield return new WaitForSeconds(trappedTime);
        Debug.Log("Player released");
    }
}
