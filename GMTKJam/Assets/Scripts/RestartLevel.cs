using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    private GameManager GM;
    public BoxCollider2D playerHead;
    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GM.hasDied = true;
            GM.StartCoroutine("RestartLevel");
        }
    }
}
