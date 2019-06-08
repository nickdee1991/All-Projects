using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintenanceRobot : MonoBehaviour {

    public GameObject text;
    private GameObject player;
    public GameObject terminal;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        transform.LookAt(player.transform);

        if (other.gameObject == player)
        {
            text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.LookAt(terminal.transform);

        if (other.gameObject == player)
        {
            text.gameObject.SetActive(false);
        }
    }
}
