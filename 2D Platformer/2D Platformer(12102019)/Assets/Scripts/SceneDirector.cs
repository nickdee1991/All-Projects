using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class SceneDirector : MonoBehaviour
{
    private Animator ActionOrson;
    private GameObject player;
    private GameObject cam;
    private GameObject playerCamera;
    public float cutsceneSpeed = 5;
    public bool isInCutscene = false;
    public GameObject dialogueBox;
    private DialogueTrigger dialogueTrigger;
    //private Cinemachine cineMachine;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Camera");
        ActionOrson = GameObject.Find("Cutscene").GetComponent<Animator>();
        dialogueBox = GameObject.Find("DialogueBox");
        dialogueTrigger = GetComponent<DialogueTrigger>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = GameObject.Find("PlayerCameraCenter");
    }

    private void Update()
    {
        if (!playerCamera)
        {
            playerCamera = GameObject.Find("PlayerCameraCenter");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isInCutscene == false)
        {
            CutsceneEnter();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isInCutscene == true)
        {
            CutsceneExit();
        }
    }

    public void CutsceneEnter()
    {
        isInCutscene = true;
        Debug.Log("Scene " + gameObject.name + " entered");
        ActionOrson.SetBool("CutsceneIn", true);
        dialogueTrigger.TriggerDialogue();
        cam.GetComponent<Camera2DFollow>().target = gameObject.transform;
    }

    public void CutsceneExit()
    {
        isInCutscene = false;
        Debug.Log("Scene " + gameObject.name + " exited");
        ActionOrson.SetBool("CutsceneIn", false);
        cam.GetComponent<Camera2DFollow>().target = playerCamera.transform;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}

