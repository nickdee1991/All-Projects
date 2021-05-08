using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private GameManager gm;
    private AudioManager aud;
    private TextMeshProUGUI text;


    public GameObject ObjectiveItem; // item to bring here
    public string ObjectiveCompleteText;
    public bool hasItem; 


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        aud = FindObjectOfType<AudioManager>();
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && hasItem && Input.GetKeyDown(KeyCode.E))
        {
            ObjectiveComplete();
        }
    }

    public void ObjectiveComplete()
    {
        // check for item in inventory
        //Objective complete
        text.text = ObjectiveCompleteText;        //activate UI
        //call Game Manager and update objective in list
        //play sound
        //play particle effect?
    }
}
