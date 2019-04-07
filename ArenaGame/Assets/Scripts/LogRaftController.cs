using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRaftController : MonoBehaviour {

    private Transform seaPlane;
    private Mesh planeMesh;
    private int closestVertexIndex = -1;

	// Use this for initialization
	void Start ()
    {
        seaPlane = GameObject.Find("Water").transform;
        //planeMesh = seaPlane.GetComponent<MeshRenderer>().;

	}
	
	// Update is called once per frame
	void Update ()
    {
        GetClosestVertex();
	}

    void GetClosestVertex()
    {
        for(int i = 0; i < planeMesh.vertexCount; i++)
        {
            if (closestVertexIndex == -1)
            {
                closestVertexIndex = i;
            }
            float distance = Vector3.Distance(planeMesh.vertices[i], transform.position);
            float closestDistance = Vector3.Distance(planeMesh.vertices[closestVertexIndex], transform.position);

            if(distance < closestDistance)
            {
                closestVertexIndex = i;
            }
        }

        transform.position = new Vector3
            (transform.position.x, 
            planeMesh.vertices[closestVertexIndex].y,
            transform.position.z);
    }
}
