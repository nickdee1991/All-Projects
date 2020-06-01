using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasementExit : MonoBehaviour
{
    private GameObject player;
    private LevelDirector levelDirector;
    private GameObject text;
    private GameObject cam;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelDirector = FindObjectOfType<LevelDirector>();
        text = GameObject.FindGameObjectWithTag("Text_Holder");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.Equals(player) && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(cam.GetComponent<CameraHold>().CameraHolding());
            text.GetComponentInChildren<Text>().enabled = true;
            text.GetComponentInChildren<Text>().text = "I need to find another way out";
            text.GetComponentInChildren<TextHolder>().textStart = true;

        }
    }
}
