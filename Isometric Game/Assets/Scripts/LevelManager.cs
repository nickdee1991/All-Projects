using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private CameraFollow camFollow;
    public Animator cameraAnim;
    public float GameOverTimer;
    public float FinishTimer = 10f;

    public bool gameOver;

    private static LevelManager instance;
    public Vector3 lastCheckPointPosBox;
    public Vector3 lastCheckPointPosTri;
    public Vector3 lastCheckPointPosWheel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerBox = GameObject.Find("PlayerBox");
        PlayerTri = GameObject.Find("PlayerTri");
        PlayerWheel = GameObject.Find("PlayerWheel");

        camFollow = FindObjectOfType<CameraFollow>();
        cameraAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        Cursor.visible = false;
        gameOver = false;
        //SwitchPlayer();
    }

    public int SwitchPlayerNumber = 0;

    private GameObject PlayerBox;
    private GameObject PlayerTri;
    private GameObject PlayerWheel;

    private void Update()
    {
        #region null checks
        if (camFollow == null)
        {
            camFollow = FindObjectOfType<CameraFollow>();
        }
        if (cameraAnim == null)
        {
            cameraAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        }
        if (PlayerBox == null)
        {
            PlayerBox = GameObject.Find("PlayerBox");
        }
        if (PlayerTri == null)
        {
            PlayerTri = GameObject.Find("PlayerTri");
        }
        if (PlayerWheel == null)
        {
            PlayerWheel = GameObject.Find("PlayerWheel");
        }
        #endregion null checks

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                SwitchPlayerNumber = 1;
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                SwitchPlayerNumber = 2;
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                SwitchPlayerNumber = 3;
            }
            SwitchPlayer();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Application Quit");
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("Application Restart");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public IEnumerator GameOver()
    {
        if (gameOver == true)
        {
        Debug.Log("GameOver screen");
        cameraAnim.SetBool("GameOver", true);
        yield return new WaitForSeconds(GameOverTimer);
        Debug.Log("Restarting game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void GaemOver()
    {
        StartCoroutine("GameOver");
        gameOver = false;
    }

    public void FinishGaem()
    {
        StartCoroutine("Finish");
    }

    public IEnumerator Finish()
    {
        cameraAnim.SetBool("Finish", true);
        yield return new WaitForSeconds(FinishTimer);
        Debug.Log("Finish, moving to next scene ");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SwitchPlayer()
    {
        //Debug.Log("Switch Player");
        switch (SwitchPlayerNumber)
        {
            case 1:
                //print("level1");
                Debug.Log("PlayerBox Active");
                camFollow.Target = PlayerBox.transform;
                PlayerBox.GetComponent<PlayerSimpleMovement>().enabled = true;
                PlayerTri.GetComponent<PlayerSimpleMovement>().enabled = false;
                PlayerWheel.GetComponent<PlayerSimpleMovement>().enabled = false;
                break;
            case 2:
                //print("level2");
                Debug.Log("PlayerTri Active");
                camFollow.Target = PlayerTri.transform;
                PlayerBox.GetComponent<PlayerSimpleMovement>().enabled = false;
                PlayerTri.GetComponent<PlayerSimpleMovement>().enabled = true;
                PlayerWheel.GetComponent<PlayerSimpleMovement>().enabled = false;
                break;
            case 3:
                //print("level3");
                Debug.Log("PlayerWheel Active");
                camFollow.Target = PlayerWheel.transform;
                PlayerBox.GetComponent<PlayerSimpleMovement>().enabled = false;
                PlayerTri.GetComponent<PlayerSimpleMovement>().enabled = false;
                PlayerWheel.GetComponent<PlayerSimpleMovement>().enabled = true;
                break;
        }
    }
}
