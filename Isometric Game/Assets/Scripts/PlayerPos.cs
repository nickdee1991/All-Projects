using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        if (gameObject.name.Equals("Player_HolderBox") && levelManager.lastCheckPointPosBox != null)
        {
            gameObject.transform.position = levelManager.lastCheckPointPosBox;
        }
        if (gameObject.name.Equals("Player_HolderTri") && levelManager.lastCheckPointPosTri != null)
        {
            gameObject.transform.position = levelManager.lastCheckPointPosTri;
        }
        if (gameObject.name.Equals("Player_HolderWheel") && levelManager.lastCheckPointPosWheel != null)
        {
            gameObject.transform.position = levelManager.lastCheckPointPosWheel;
        }
    }
}
