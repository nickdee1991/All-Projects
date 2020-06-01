using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cabin : MonoBehaviour
{
    GameObject player;
    GameObject frontWall;
    private GameObject cam;
    private bool fireplaceText;
    private Animator Anim;
    private GameObject text;
    private Transform inCabin;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        fireplaceText = false;
        frontWall = GameObject.Find("frontWall");
        player = GameObject.FindGameObjectWithTag("Player");
        Anim = GameObject.Find("Main Camera").GetComponent<Animator>();
        text = GameObject.FindGameObjectWithTag("Text_Holder");
        inCabin = GameObject.Find("cabinCamPos").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(player) && fireplaceText == false)
        {
            frontWall.SetActive(false);
            cam.transform.position = inCabin.transform.position;
            cam.transform.rotation = inCabin.transform.rotation;
            cam.transform.parent = null;
            Debug.Log(text.GetComponentInChildren<Text>().text);

            text.GetComponentInChildren<Text>().enabled = true;
            text.GetComponentInChildren<Text>().text = "I'll need to get this door closed and start that fire.";
            text.GetComponentInChildren<TextHolder>().textStart = true;
            fireplaceText = true;
        }
    }
}
