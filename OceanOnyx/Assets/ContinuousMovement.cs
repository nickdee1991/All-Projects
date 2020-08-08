using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    public XRNode inputSource;
    private Vector2 inputAxis;

    private XRRig rig;
    private CharacterController character;
    public float speed = 1; // variable speed for player movement
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>(); //reference the character controller
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate()
    {
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y,0); // 
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y); // direction player will move in

        character.Move(direction * Time.fixedDeltaTime * speed); // to initialise player movement
    }
}
