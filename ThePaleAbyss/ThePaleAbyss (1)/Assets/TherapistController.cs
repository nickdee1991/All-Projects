using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TherapistController : MonoBehaviour
{
    public DialogueTrigger dialogueTriggerA;
    public DialogueTrigger dialogueTriggerB;
    public DialogueTrigger dialogueTriggerC;
    public DialogueTrigger dialogueTriggerD;
    public DialogueTrigger dialogueTriggerE;
    public DialogueManager dialogueMgr;
    private GameObject player;


    private GameObject cam;
    public InteractableManager IntMgr;
    private LevelDirector levDir;

    public void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        IntMgr = FindObjectOfType<InteractableManager>();
        dialogueMgr = FindObjectOfType<DialogueManager>();
        levDir = FindObjectOfType<LevelDirector>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.Equals(player))
        {
            EndingChoice();
        }
    }
    private void Update()
    {
        if (dialogueMgr.EndOfDialogue)
        {
            cam.GetComponent<Animator>().SetBool("CameraCut", true);
            SceneManager.LoadScene("Main");
        }
            
    }

    public void EndingChoice()
    {
        switch (IntMgr.chosenEnding)
        {

            case InteractableManager.Ending.Start: // first playthrough
                dialogueTriggerA.TriggerDialogue();
                //add drugging effect before restart
                Debug.Log(IntMgr.chosenEnding);
                break;

            case InteractableManager.Ending.NoItem: // second play - didnt pickup item
                dialogueTriggerB.TriggerDialogue();
                //add black screen at the end "I descend once more"
                Debug.Log(IntMgr.chosenEnding);
                break;

            case InteractableManager.Ending.Item:   // second play - picked up item
                dialogueTriggerC.TriggerDialogue();
                //add black screen at the end "And so i tore out a piece of the abyss"
                //
                Debug.Log(IntMgr.chosenEnding);
                break;

            case InteractableManager.Ending.ItemDestroyed: // second play - destroyed item
                dialogueTriggerD.TriggerDialogue();
                Debug.Log(IntMgr.chosenEnding);
                break;

            case InteractableManager.Ending.ItemBoss:   // second play - destroyed claudia
                dialogueTriggerE.TriggerDialogue();
                Debug.Log(IntMgr.chosenEnding);
                break;
        }
    }
}
