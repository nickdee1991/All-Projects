using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float movementSpeed;
    public float sensitivity = 2f;
    public float waitTime;
    public float rotationSpeed = 5f;

    public GameObject playerObj;
    public AudioSource audioDeath;

    private Rigidbody rb;
    public Camera cam;
    public RaycastHit hitInfo;

    private void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        Vector3 vel = rb.velocity;
    }

    public void Update()
    {
        #region Player Movement

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime); 
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            transform.Rotate(Vector3.up * -movementSpeed * 15 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.C))
        {
            transform.Rotate(Vector3.up * movementSpeed * 15 * Time.deltaTime);
        }

        #endregion End Player Movement

    }

    public void CameraScreenPointToRay()
    {
        //player facing mouse
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

}
