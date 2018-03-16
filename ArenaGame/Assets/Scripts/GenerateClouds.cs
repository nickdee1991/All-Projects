using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CloudData
{
    public Vector3 pos;

    public Vector3 scale;

    public Quaternion rot;

    private bool _isActive;


    // prevents other classes from directly setting isActive var
    public bool isActive
    {
        get
        {
            return _isActive;
        }
    }

    public int x;

    public int y;

    public float distFromCam;

    //returns the matrix 4x4 of our cloud

    public Matrix4x4 matrix
    {
        get
        {
            return Matrix4x4.TRS(pos, rot, scale);
        }
    }
}

public class GenerateClouds : MonoBehaviour {


}
