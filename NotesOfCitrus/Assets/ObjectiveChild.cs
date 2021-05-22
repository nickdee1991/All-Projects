using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveChild : MonoBehaviour
{
    private GameObject player;
    private AudioManager aud;
    private GameManager gm;
    public ObjectiveParent Parent;
    public string ObjectName;
    private TextMeshProUGUI text;

    private bool hasPickedUp;
    private bool playerInRange;


    // Start is called before the first frame update
    void Start()
    {
        aud = FindObjectOfType<AudioManager>();
        gm = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        text = player.GetComponentInChildren<TMPro.TextMeshProUGUI>();

        hasPickedUp = false;

        if (text.enabled == true)
        {
            text.enabled = false;
        }

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
        if (Input.GetKeyDown(KeyCode.E)&& hasPickedUp == false)
        {
            hasPickedUp = true;
            Parent.hasItem = true; //update in gamemanager
            gameObject.GetComponent<MeshRenderer>().enabled = false;//pickup this object
            text.enabled = true;//enable text
            text.text = ObjectName + " picked up"; //text = "*This item* picked up"
        }

        if (playerInRange && hasPickedUp == false)
        {
            text.enabled = true;//enable text
            text.text = ObjectName;
        }
    }
    private void OnMouseExit()
    {
        text.enabled = false;//enable text
    }
}
