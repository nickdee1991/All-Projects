using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerProjectile))]
public class weaponPickup : MonoBehaviour
{
    private void Start()
    {
        player1.GetComponent<PlayerProjectile>();
        player2.GetComponent<PlayerProjectile>();
        player1ammo = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerProjectile>().throwableAmmo;
    }
    public float player1ammo;
    public GameObject player1;
    public GameObject player2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player1"))
        {
            Debug.Log("Player1 ammo = " + player1ammo);
            player1ammo = 3;
            player1.GetComponent<PlayerProjectile>() .throwable1.SetActive(true);
            player1.GetComponent<PlayerProjectile>().throwable2.SetActive(true);
            player1.GetComponent<PlayerProjectile>().throwable3.SetActive(true);
        }else

        if (other.gameObject.tag.Equals("Player2"))
        {
            Debug.Log(player2.GetComponent<PlayerProjectile>().throwableAmmo);
            player2.GetComponent<PlayerProjectile>().throwableAmmo = 3;
            player2.GetComponent<PlayerProjectile>().throwable1.SetActive(true);
            player2.GetComponent<PlayerProjectile>().throwable2.SetActive(true);
            player2.GetComponent<PlayerProjectile>().throwable3.SetActive(true);
        }

        if (other.gameObject.tag.Equals("Player1(Clone)"))
        {
            Debug.Log("Player1 ammo = " + player1.GetComponent<PlayerProjectile>().throwableAmmo);
            player1.GetComponent<PlayerProjectile>().throwableAmmo = 3;
            player1.GetComponent<PlayerProjectile>().throwable1.SetActive(true);
            player1.GetComponent<PlayerProjectile>().throwable2.SetActive(true);
            player1.GetComponent<PlayerProjectile>().throwable3.SetActive(true);
        }
        else

        if (other.gameObject.tag.Equals("Player2(Clone)"))
        {
            Debug.Log(player2.GetComponent<PlayerProjectile>().throwableAmmo);
            player2.GetComponent<PlayerProjectile>().throwableAmmo = 3;
            player2.GetComponent<PlayerProjectile>().throwable1.SetActive(true);
            player2.GetComponent<PlayerProjectile>().throwable2.SetActive(true);
            player2.GetComponent<PlayerProjectile>().throwable3.SetActive(true);
        }
    }
}
