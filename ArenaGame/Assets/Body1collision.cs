using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body1collision : MonoBehaviour
{
    public GameObject Player1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Projectile"))
        {
            Debug.Log("Player1 ammo = " + Player1.GetComponent<PlayerProjectile>().throwableAmmo);
            //Destroy(other.gameObject);
            Player1.GetComponent<PlayerProjectile>().throwableAmmo ++;
        }
    }
}
