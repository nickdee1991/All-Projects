using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    private LevelDirector levDir;
    private GameObject player;
    private InteractableManager intMgr;
    public bool respawnPlayer;
    void Start()
    {
        levDir = FindObjectOfType<LevelDirector>();
        player = GameObject.FindGameObjectWithTag("Player");
        intMgr = FindObjectOfType<InteractableManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(player))
        {
            if (intMgr.inBasement)
            {
                respawnPlayer = true;
                levDir.InBasement();
                Debug.Log("respawning in basement");
            }
            if (intMgr.inTherapist)
            {
                respawnPlayer = true;
                levDir.InTherapist();
                Debug.Log("respawning in therapist");
            }else if (!intMgr.inBasement && !intMgr.inTherapist)
            {
                levDir.RestartGame();
            }

        }

    }
}
