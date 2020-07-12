using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int moveSpeed;
    public int rotateSpeed;
    public GameObject wheel;
    public int wheelRotation;

    public int level;

    void Awake()
    {
        level = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        CheckLevel();
    }

    void CheckLevel()
    {
        switch (level)
        {
            case 1:
                //print("level1");

                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.Self);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.Self);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime, Space.Self);
                }
                break;
            case 2:
                //print("level2");
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.Self);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime, Space.Self);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.Self);
                }
                break;
            case 3:
                //print("level3");
                if (Input.GetKey(KeyCode.W))
                {                   
                    transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.Self);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime, Space.Self);                 
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.Self);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
                }
                break;
            case 4:
                //print("level4");
                if (Input.GetKey(KeyCode.W))
                {                   
                    transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime, Space.Self);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.Self);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.Self);
                }
                break;

        };
    }

    void LateUpdate()
    {

        #region input
        if (Input.anyKey)
        {
            CheckLevel();
            wheel.transform.Rotate(new Vector3(0, Time.deltaTime * -wheelRotation, 0));
        }
        #endregion
    }
}
