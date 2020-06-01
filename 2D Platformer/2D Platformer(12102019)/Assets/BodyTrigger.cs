using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTrigger : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInChildren<Animator>().SetBool("isEatingStart",true);
            collision.gameObject.GetComponentInChildren<Animator>().SetBool("isEatingLoop", true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInChildren<Animator>().SetBool("isEatingStart", false);
            collision.gameObject.GetComponentInChildren<Animator>().SetBool("isEatingLoop", false);
        }
    }
}
