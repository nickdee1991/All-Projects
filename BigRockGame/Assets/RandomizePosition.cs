using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePosition : MonoBehaviour
{
    public GameObject sphere;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position += new Vector3(
            Random.Range(-4f, 4f),
            Random.Range(-4f, 4f),
            0f);

            sphere.transform.position += new Vector3(
            Random.Range(-6f, 6f),
            Random.Range(-6f, 6f),
            0f);
        }
    }
}

