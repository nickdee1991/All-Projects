using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    private MeshRenderer mesh;
    private GameManager gm;

    public float rotation = 50;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        gm = FindObjectOfType<GameManager>();

        if (gm.DEBUGMODE == true)
        {
            transform.Rotate(new Vector3(0, Time.deltaTime * rotation, 0));
        }else{
            mesh.enabled = false;
        }

    }
}
