using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    private GameObject player;
    private GameObject itemPlayer;
    private InteractableManager IntMgr;
    private GameObject ItemGraphic;
    public ParticleSystem fire;
    public ParticleSystem smoke;
    private AudioManager Aud;

    private void Start()
    {
        itemPlayer = GameObject.Find("ItemPlayer");
        IntMgr = GameObject.FindGameObjectWithTag("InteractableManager").GetComponent<InteractableManager>();
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
            fire.Play();
            smoke.Play();
            Aud.PlaySound("Fire");
            IntMgr.itemCollected = true;
            IntMgr.chosenEnding = InteractableManager.Ending.Item; // set ending and therapist dialogue once collected
        }
    }
}
