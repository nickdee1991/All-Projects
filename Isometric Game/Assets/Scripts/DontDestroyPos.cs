using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyPos : MonoBehaviour
{
    private static DontDestroyPos instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
