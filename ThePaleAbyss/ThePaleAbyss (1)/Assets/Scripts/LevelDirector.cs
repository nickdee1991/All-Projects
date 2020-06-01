using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelDirector : MonoBehaviour
{
    public Transform playerBasementSpawn;
    public Transform playerTherapistSpawn;
    public Transform playerCabinSpawn;
    public Transform camOriginalPos;
    public Transform camBasementPos;
    public Transform camTherapistPos;
    public Sprite playerBasementSprite;

    private openWoodHatch openWoodHatch;
    private InteractableManager IntMgr;
    public Animator anim;
    private GameObject cam;
    private GameObject player;
    public GameObject GameUI;
    public GameObject GameOverUI;
    private AudioManager Aud;
    public DialogueManager dialogueManager;
    public AudioManager AudMgr;
    private TherapistController therCont;
    private LevelBoundary levBound;

    public bool cursorLocked;
    public bool movePlayer;
    public bool hasFaded;
    public bool GameOver;
    public bool BasementSound;

    private float pauseSpeed;
    private float gameSpeed;

    private void Start()
    {
        BasementSound = false;
        GameOver = false;
        GameOverUI.SetActive(false);
        cursorLocked = false;
        hasFaded = false;
        dialogueManager = FindObjectOfType<DialogueManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GameObject.Find("PlayerGraphics").GetComponent<Animator>();
        IntMgr = GameObject.FindGameObjectWithTag("InteractableManager").GetComponent<InteractableManager>();
        AudMgr = FindObjectOfType<AudioManager>();
        Aud = FindObjectOfType<AudioManager>();
        openWoodHatch = FindObjectOfType<openWoodHatch>();
        therCont = FindObjectOfType<TherapistController>();
        levBound = FindObjectOfType<LevelBoundary>();
        playerBasementSpawn = GameObject.Find("BasementSpawn").transform;
        playerTherapistSpawn = GameObject.Find("TherapistSpawn").transform;
        camOriginalPos = GameObject.Find("CameraOriginalPos").transform;
        camBasementPos = GameObject.Find("CameraBasementPos").transform;
        camTherapistPos = GameObject.Find("CameraTherapistPos").transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        cam.GetComponent<Animator>().SetBool("CameraStart",true);

        gameSpeed = player.GetComponent<Player>().movementSpeed;
        pauseSpeed = player.GetComponent<Player>().pauseSpeed;
        movePlayer = false;

        GameUI.SetActive(true);
        CursorUnlocked();
    }
    private void Update()
    {
        if (IntMgr.inBasement)
        {
            InBasement();
        }
        if (IntMgr.inTherapist)
        {
            InTherapist();
        }
    }

    public void CursorLocked()
    {
        Debug.Log("cursor locked");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponent<Player>().movementSpeed = gameSpeed;
    }
    public void CursorUnlocked()
    {
        Debug.Log("cursor unlocked, ui active");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponent<Player>().movementSpeed = pauseSpeed;
    }

    public void InBasement()
    {
        anim.SetBool("inBasement", true);

        if (!BasementSound)
        {
            AudMgr.PlaySound("Basement");
            BasementSound = true;
        }

        if (!movePlayer || levBound.respawnPlayer)
        {
            player.transform.position = playerBasementSpawn.transform.position;
            cam.transform.parent = player.transform;
            cam.transform.position = camBasementPos.transform.position;
            RenderSettings.fogColor = Color.black;
            movePlayer = true;
        }
    }

    public void InTherapist()
    {
        anim.SetBool("inBasement", false);
        if (!movePlayer || levBound.respawnPlayer)
        {
            player.transform.position = playerTherapistSpawn.transform.position;
            //cam.transform.parent = player.transform;
            cam.transform.position = camTherapistPos.transform.position;
            cam.transform.rotation = camTherapistPos.transform.rotation;
            player.transform.rotation = transform.rotation;
            RenderSettings.fog = false;
            movePlayer = true;
        }
    }

    public void StartGame(string StartGame)
    {
        Debug.Log("Start Game");
        cursorLocked = true;
        cam.GetComponent<Animator>().SetBool("CameraStart", false);
        CursorLocked();
        GameUI.SetActive(false);
    }
    public void EndGame()
    {
        Debug.Log("End Game");
        Application.Quit();
        cursorLocked = true;
    }
    public void RestartGame()
    {
        Debug.Log("Restart Game");
        SceneManager.LoadScene("Main");
        cursorLocked = true;
        CursorLocked();
    }

    public void PlayerDead()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        CameraFade();        //black screen
        cam.GetComponent<Animator>().SetBool("CameraCut", true);        //pause
        GameOverUI.SetActive(true);        //restart game or exit

        if (GameOver)
        {
            Debug.Log("PLAYER DEAD");
            Aud.PlaySound("Dead");        //death audio
            GameOver = false;
        }
    }

     public IEnumerator CameraFade()
    {
        //fade camera
        cam.GetComponent<Animator>().SetBool("CameraFade", true);
        yield return new WaitForSeconds(3);
        cam.GetComponent<Animator>().SetBool("CameraFade", false);
        hasFaded = true;
        //return
    }
}
