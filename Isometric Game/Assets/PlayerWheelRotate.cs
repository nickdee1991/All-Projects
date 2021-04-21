using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWheelRotate : MonoBehaviour
{
    public GameObject wheel;
    public float wheelRotation;
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.SwitchPlayerNumber == 3 && Input.anyKey)
        {
            wheel.transform.Rotate(new Vector3(0, Time.deltaTime * wheelRotation, 0));
        }

    }
}
