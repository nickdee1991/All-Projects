using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPour : MonoBehaviour
{
    public ParticleSystem liquid;
    public Color liquidColour;

    public bool includeChildren = true;

    private void Update()
    {
        ObjectIsPouring(); // check if object the player is holding is inverted

        //set the particlesystem color to liquidColour variable
        var main = liquid.main;
        main.startColor = liquidColour;
    }

    void ObjectIsPouring()
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
        else
        {
            liquid.Stop();
            liquid.Clear();
            if (liquid.isPlaying)
            {
                liquid.Stop();
            }
        }
    }
}
