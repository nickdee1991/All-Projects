using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    private GameObject player;
    private GameObject ItemGraphic;
    private AudioManager Aud;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ItemGraphic = GameObject.Find("ItemGraphic");
        Aud = FindObjectOfType<AudioManager>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(player) && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Item Collected");
            ItemGraphic.SetActive(false); // turn off item renderer
        }
    }
}
