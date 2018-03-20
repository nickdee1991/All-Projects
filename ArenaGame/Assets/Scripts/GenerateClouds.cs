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

    public CloudData(Vector3 pos, Vector3 scale, Quaternion rot, int x, int y, float distFromCam)
    {
        this.pos = pos;
        this.scale = scale;
        this.rot = rot;
        SetActive(true);
        this.x = x;
        this.y = y;
        this.distFromCam = distFromCam;
    }

    public void SetActive (bool desState)
    {
        _isActive = desState;
    }
}



public class GenerateClouds : MonoBehaviour
{
    public Mesh cloudMesh;

    public Material cloudMat;

    //Cloud Data 

    public float cloudSize = 5;

    public float maxScale = 1;

    //Noise generation 

    public float timeScale = 1;

    public float texScale = 1;

    //Cloud Scaling Info

    public float minNoiseSize = 0.5f;

    public float sizeScale = 0.25f;

    //Culling Data

    public Camera cam;

    public int maxDist;

    //Number of batches

    public int batchesToCreate;

    private Vector3 prevCamPos;

    private float offsetX = 1;

    private float offsetY = 1;

    private List<List<CloudData>> batches = new List<List<CloudData>>();

    private List<List<CloudData>> batchesToUpdate = new List<List<CloudData>>();

    private void Start()
    {
        for (int batchesX = 0; batchesX < batchesToCreate; batchesX++)
        {
            for (int batchesY = 0; batchesY < batchesToCreate; batchesY++)
            {
                BuildCloudBatch(batchesX, batchesY);
            }
        }
    }

    //start by looping through our x and y values to generate a batch thats 31x31 clouds
    //limited due to 1024 max of graphics.drawmeshinstanciated
    private void BuildCloudBatch(int xLoop, int yLoop)
    {
        //mark a batch if its within range of our camera
        bool markBatch = false;
        //this is our current cloud batch that we're brewing
        List<CloudData> currBatch = new List<CloudData>();

        for (int x = 0; x < 31; x++)
        {
            for (int y = 0; y < 31; y++)
            {
                //add a cloud for each loop
                AddCloud(currBatch, x + xLoop * 31, y + yLoop * 31);
            }
        }

        markBatch = CheckForActiveBatch(currBatch);

        //add the newest batch to the batches list
        batches.Add(currBatch);

        //if the batch is marked addd it to the batchesToUpdate list
        if (markBatch) batchesToUpdate.Add(currBatch);
    }

    //this method checks to see if the current batch has a cloud that is within our cameras range
    //return true if a cloud is within range
    //return false if no clouds are within range

    private bool CheckForActiveBatch(List<CloudData> batch)
    {
        foreach (var cloud in batch)
        {
            cloud.distFromCam = Vector3.Distance(cloud.pos, cam.transform.position);
            if (cloud.distFromCam < maxDist) return true;
        }
        return false;
    }

    //this method creates our clouds as a clouddata object
    private void AddCloud(List<CloudData> currBatch, int x, int y)
    {
        //first we set our new clouds position
        Vector3 position = new Vector3(transform.position.x + x * cloudSize, transform.position.y, transform.position.z + y * cloudSize);

        //we set our new clouds distance to the camera so we can use it later
        float distToCam = Vector3.Distance(new Vector3(x, transform.position.y, y), cam.transform.position);

        //finally we add our new CLoudData cloud to the current batch
        currBatch.Add(new CloudData(position, Vector3.zero, Quaternion.identity, x, y, distToCam));

    }
    private void Update()
    {
        MakeNoise();
        offsetX += Time.deltaTime * timeScale;
        offsetY += Time.deltaTime * timeScale;
    }


    //this method updates our noise/clouds
    //first we check to see if the camera has moved
    //if it hasnt we update batches
    //if it has move we need to reset the prevCamPos along with updating our batch list before updating our batches
    //TODO: set allowed movement range to camera so the player can move a small amount without causing a full batch list reset

   private void MakeNoise()
    {
        if ( cam.transform.position == prevCamPos)
        {
            UpdateBatches();
        }
        else
        {
            prevCamPos = cam.transform.position;
            UpdateBatchList();
            UpdateBatches();
        }
        RenderBatches();
        prevCamPos = cam.transform.position;
    }

    //this method updates our clouds
    //first we loop through all batches in the batchesToUpdate list
    //for each batch we need to get each clouds with another loop

    private void UpdateBatches()
    {
        foreach (var batch in batchesToUpdate)
        {
            foreach (var cloud in batch)
            {
                //get noise size based on clouds pos, noise texture scale, and our offset amount
                float size = Mathf.PerlinNoise(cloud.x * texScale + offsetX, cloud.y * texScale + offsetY);

                //if our cloud has a size that's above our visible cloud threshold we need to show it
                if (size > minNoiseSize)
                {
                    //get the current scale of the cloud
                    float localScaleX = cloud.scale.x;

                    //activate any clouds
                    if (!cloud.isActive)
                    {
                        cloud.SetActive(true);
                        cloud.scale = Vector3.zero;
                    }
                    //if not max size, scale up
                    if (localScaleX < maxScale)
                    {
                        ScaleCloud(cloud, 1);

                        //limit our max size
                        if (cloud.scale.x > maxScale)
                        {
                            cloud.scale = new Vector3(maxScale, maxScale, maxScale);
                        }
                    }
                }

                //active and it shouldn't be, lets scale down
                else if (size < minNoiseSize)
                {
                    float localScaleX = cloud.scale.x;
                    ScaleCloud(cloud, -1);

                    //when the cloud is really small we can just set it to 0 and hide it
                    if (localScaleX <= 0.1)
                    {
                        cloud.SetActive(false);
                        cloud.scale = Vector3.zero;
                    }
                }
            }
        }
    }

    void ScaleCloud(CloudData cloud, int direction)
    {
        cloud.scale += new Vector3(sizeScale * Time.deltaTime * direction, sizeScale * Time.deltaTime * direction, sizeScale * Time.deltaTime * direction);
    }

    private void UpdateBatchList()
    {
        //clears our list 
        batchesToUpdate.Clear();

        //loop through all the generated batches
        foreach (var batch in batches)
        {
            //if a single cloud is within range we need to add the batch to the update list
            if (CheckForActiveBatch(batch))
            {
                batchesToUpdate.Add(batch);
            }
        }
    }
    private void RenderBatches()
    {
        foreach (var batch in batchesToUpdate)
        {
            Graphics.DrawMeshInstanced(cloudMesh, 0, cloudMat, batch.Select((a) => a.matrix).ToList());
        }
    }
}    


