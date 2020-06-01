using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject nextButton;
    public Queue<string> sentences;
    public AudioManager AudMgr;

    public bool inDialogue;
    public bool EndOfDialogue;

    void Start()
    {
        AudMgr = FindObjectOfType<AudioManager>();
        sentences = new Queue<string>();
        EndOfDialogue = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        inDialogue = true;
        Debug.Log("starting conversation with " + dialogue.name);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        nameText.enabled = true;
        dialogueText.enabled = true;
        nextButton.SetActive(true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        AudMgr.PlaySound("Speech");
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        Debug.Log(dialogueText.text);
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        Debug.Log("End of Conversation");
        nameText.enabled = false;
        dialogueText.enabled = false;
        nextButton.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inDialogue = false;
        EndOfDialogue = true;
    }
}
