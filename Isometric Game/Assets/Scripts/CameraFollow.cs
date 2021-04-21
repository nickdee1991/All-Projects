using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    public Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool LookAtTarget;

    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - Target.position;
    }

    private void LateUpdate()
    {
        Vector3 newPos = Target.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        if (LookAtTarget)
        {
            transform.LookAt(Target);
        }
    }
}
