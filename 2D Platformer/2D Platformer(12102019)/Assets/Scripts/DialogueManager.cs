using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Image portrait;
    private Camera2DFollow cam2d;
    private SceneDirector sceneDir;
    public Queue<string> sentences;

    void Start()
    {
        sceneDir = FindObjectOfType<SceneDirector>();
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("starting conversation with " + dialogue.name);
        nameText.text = dialogue.name;
        portrait.sprite = dialogue.sprite;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        
        if(sentences.Count == 0)
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
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of Conversation");
        GameObject.Find("Cutscene").GetComponent<Animator>().SetBool("CutsceneIn", false);
        sceneDir.CutsceneExit();
    }
}
