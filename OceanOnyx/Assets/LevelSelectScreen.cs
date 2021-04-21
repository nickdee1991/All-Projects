using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScreen : MonoBehaviour
{
    private float PlayerDefaultSpeed;
    private AudioManager aud;
    public GameObject LevelSelectUI;

    private void Start()
    {
        aud = FindObjectOfType<AudioManager>();
        PlayerDefaultSpeed = FindObjectOfType<PlayerMovement>().movementSpeed;
        Debug.Log(PlayerDefaultSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<PlayerInteraction>().IsInteracting = true;
    }

    private void OnTriggerExit(Collider other)
    {
        FindObjectOfType<PlayerInteraction>().IsInteracting = false;
    }

    public void LeaveMenu()
    {
        FindObjectOfType<PlayerInteraction>().IsInteracting = false;
        LevelSelectUI.SetActive(false); //enable LevelSelectScreenUI
        FindObjectOfType<PlayerMovement>().movementSpeed = PlayerDefaultSpeed; //enable player movement
        Cursor.lockState = CursorLockMode.Locked; //disable mouse ? or keyboard controls
        Cursor.visible = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Level Select Screen");
            FindObjectOfType<PlayerInteraction>().IsInteracting = false;
            aud.PlaySound("Interacting");
            LevelSelectUI.SetActive(true); //enable LevelSelectScreenUI
            FindObjectOfType<PlayerMovement>().movementSpeed = 0; //disable player movement
            Cursor.lockState = CursorLockMode.None; //enable mouse ? or keyboard controls
            Cursor.visible = true;
        }
    }
}
