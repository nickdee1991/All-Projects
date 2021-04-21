using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject OverheadIndicator;
    public bool IsInteracting;

    // Start is called before the first frame update
    void Start()
    {
        IsInteracting = false;
    }

    private void FixedUpdate()
    {
        if (IsInteracting)
        {
            OverheadIndicator.SetActive(true);
        }
        else
        {
            OverheadIndicator.SetActive(false);
        }
    }
}
