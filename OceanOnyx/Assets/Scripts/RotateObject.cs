using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateSpeed;
    public enum axis {x,y,z}

    public axis chosenAxis;

    private void Update()
    {
        RotateAxis();
    }

    public void RotateAxis()
    {
        switch (chosenAxis)
        {
            case axis.x:
                transform.Rotate(new Vector3(Time.deltaTime * rotateSpeed, 0, 0));
                break;
            case axis.y:
                transform.Rotate(new Vector3(0, Time.deltaTime * rotateSpeed, 0));
                break;
            case axis.z:
                transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotateSpeed));
                break;
        }
    }
}
