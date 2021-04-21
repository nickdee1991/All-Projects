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
    private void Update()
    {
        //CheckLevel();
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
            case 5:
                //introduce swinging doors and auto forwards

                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);

                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.Self);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.Self);
                    transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.Self);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime, Space.Self);
                    transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.Self);
                }
                break;
            case 6:
                //introduce buttons and moving platforms along with second camera angle
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
            case 7:
                //several moving platforms and two button for stopping platforms to align with end door 
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
            case 8:
                // multi room level with several cameras and timed door puzzle
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
            case 9:
                //puzzle to push box that triggers bomb opening hole in wall that robot will escape to
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
            case 10:
                //last level where you move robot to computer console to download auto-roam feature 
                //allowing robot to escape out sewer pipe and into the world with sensitive zagorkzkiy information
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
