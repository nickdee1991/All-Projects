using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public LayerMask viewMask;

    public Light spotlight;

    Color originalSpotlightColour;

    public float viewDistance;
    public float viewAngle;

    private float playerVisibleTimer;
    private float currentTime;

    private int destPoint = 0;

    public bool detectedEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
