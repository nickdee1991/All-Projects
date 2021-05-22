using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveParent : MonoBehaviour
{
    public bool IsLevelObjective; // this ObjectiveParent will advance the player to the next level

    private GameObject player;
    private GameManager gm;
    private AudioManager aud;
    private TextMeshProUGUI text;


    public GameObject ObjectiveItem; // item to bring here
    public string ObjectiveCompleteText;
    public string ObjectiveParentName;
    public bool hasItem;
    private bool playerInRange;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        text = player.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        aud = FindObjectOfType<AudioManager>();
        gm = FindObjectOfType<GameManager>();

        playerInRange = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnMouseOver()
    {

        if (playerInRange)
        {
            text.enabled = true;
            text.text = ObjectiveParentName;
            if (hasItem && Input.GetKeyDown(KeyCode.E))
            {
                ObjectiveComplete();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerInRange = false;
        text.enabled = false;
    }

    public void ObjectiveComplete()
    {
        //check for item in inventory
        text.enabled = true;        //Objective complete
        text.text = ObjectiveCompleteText;        //activate UI
        //play sound
        //play particle effect?

        if (IsLevelObjective == true)
        {
            gm.LevelComplete();
        }
    }
}
