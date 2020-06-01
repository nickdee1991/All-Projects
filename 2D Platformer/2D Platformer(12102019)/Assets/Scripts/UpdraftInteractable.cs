using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class UpdraftInteractable : MonoBehaviour
{
    public GameObject playerWeapon;
    public GameObject player;
    private Rigidbody2D playerRB;
    public float upwardBoost;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
        playerWeapon = GameObject.FindGameObjectWithTag("Weapon");
    }

    private void Update()
    {
        if (player == null && playerWeapon == null && playerRB == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerRB = player.GetComponent<Rigidbody2D>();
            playerWeapon = GameObject.FindGameObjectWithTag("Weapon");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetMouseButton(1) && other.transform.gameObject.Equals(playerWeapon) && player.GetComponent<PlatformerCharacter2D>().m_Grounded == false)
        {
            if (playerWeapon.GetComponent<Weapon>().isTopCollider == true)
            {
                playerRB.AddForce(Vector2.up * upwardBoost);
                playerRB.drag = 0;
            }
        }
    }
}
