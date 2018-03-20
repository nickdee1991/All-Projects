using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body2collision : MonoBehaviour
{
    public GameObject Player2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Projectile"))
        {
            Debug.Log("Player2 ammo = " + Player2.GetComponent<PlayerProjectile>().throwableAmmo);
            //Destroy(gameObject);
            Player2.GetComponent<PlayerProjectile>().throwableAmmo ++;
        }
    }
}
