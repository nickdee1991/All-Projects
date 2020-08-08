using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPour : MonoBehaviour
{
    public ParticleSystem liquid;

    public bool includeChildren = true;

    private void Update()
    {
        if (Vector3.Dot(transform.up, Vector3.down) > 0)
        {
            liquid.Emit(1);
            //liquid.Play();
            if (liquid.isStopped)
            {
                liquid.Emit(1);
                //Debug.Log("BOTTLE IS INVERTED");
            }
        }
        else{
            liquid.Stop();
            liquid.Clear();
            if (liquid.isPlaying)
            {
                liquid.Stop();
            }
        }
    }
}
