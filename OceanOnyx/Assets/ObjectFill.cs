using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFill : MonoBehaviour
{
    private MeshRenderer fillMesh;
    private float fillTimer;

    private float fillSpeed = 2;
    private float fillAmount;

    private void Start()
    {
        fillTimer = 0;
    }

    private void OnParticleTrigger()
    {
        if (gameObject.CompareTag("Interactable"))
        {
            fillMesh = gameObject.GetComponentInChildren<MeshRenderer>();
            fillTimer += Time.deltaTime + fillSpeed;
            Debug.Log(fillTimer);
        }
    }
}
