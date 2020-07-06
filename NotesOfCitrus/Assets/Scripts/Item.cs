using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private GameManager GM;
    private GameObject player;
    private MeshRenderer ItemGraphic;
    private AudioManager Aud;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        ItemGraphic = GetComponent<MeshRenderer>();
        Aud = FindObjectOfType<AudioManager>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.Equals(player) && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Item Collected");
            ItemGraphic.enabled = false; // turn off item renderer
            Aud.PlaySound("Pickup");

            if (gameObject.name.Equals("ObjKey"))
            {
                GM.EndKey = true;
                GameObject.Find("ObjKeyHand").GetComponent<MeshRenderer>().enabled = true;
                GetComponent<Light>().enabled = false;
            }
            if (gameObject.name.Equals("ObjHammer"))
            {
                GM.Hammer = true;
                GameObject.Find("ObjHammerHand").GetComponent<MeshRenderer>().enabled = true;
                GetComponent<Light>().enabled = false;
            }
            if (gameObject.name.Equals("ObjChisel"))
            {
                GM.Chisel = true;
                GameObject.Find("ObjChiselHand").GetComponent<MeshRenderer>().enabled = true;
                GetComponent<Light>().enabled = false;
            }
        }
    }
}
