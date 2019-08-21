using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenKey : MonoBehaviour {

    private GameObject player;

    public GameObject floatingText;

    private void OnMouseEnter()
    {
        floatingText.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        floatingText.gameObject.SetActive(false);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseDown()
    {
        player.GetComponent<Player>().hasGardenKey = true;
        Destroy(gameObject, 0.5f);
        floatingText.gameObject.SetActive(false);
        Debug.Log("Garden Key = " + player.GetComponent<Player>().hasGardenKey);
    }
}
