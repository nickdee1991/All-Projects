using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    //internal static readonly Camera main;

    //Variables
    public Transform player;
    public float smooth;

    public float mouseSensitivity;

    float xAxisClamp = 0.0f;

    public float height;
    public float dist;

    public bool ThirdPersonCam;

    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        ThirdPersonCam = false;
    }

    //Methods
    private void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;

        xAxisClamp -= rotAmountY;

        Vector3 targetRotCam = transform.rotation.eulerAngles;
        Vector3 targetRotBody = player.rotation.eulerAngles;

        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0;
        targetRotBody.y += rotAmountX;

        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            targetRotCam.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            targetRotCam.x = 270;
        }


        transform.rotation = Quaternion.Euler(targetRotCam);
        player.rotation = Quaternion.Euler(targetRotBody);

        if (Input.GetKeyDown(KeyCode.C))
        {
            ThirdPersonCamera();
        }


    }

    public void ThirdPersonCamera ()
    {
        Vector3 pos = new Vector3();
        pos.x = this.transform.position.x;
        pos.z = this.transform.position.z - dist;
        pos.y = this.transform.position.y + height;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);       
    }
}
