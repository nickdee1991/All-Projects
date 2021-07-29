using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveParent : MonoBehaviour
{
    public bool IsLevelObjective; // this ObjectiveParent will advance the player to the next level

    private GameObject player;
    private GameManager gm;
    private AudioManager aud;
    private TextMeshProUGUI text;
    private Animator textAnim;

    public GameObject[] Objectives;
    private float ObjectiveTimerWait = 1f;

    public string ObjectiveCompleteText;
    public string ObjectiveParentName;
    public float textFadeTime = 1;
    public int ObjectivesToComplete;
    public bool hasItem;
    private bool playerInRange;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        text = GameObject.FindGameObjectWithTag("UItext").GetComponentInChildren<TextMeshProUGUI>();
        textAnim = GameObject.FindGameObjectWithTag("UItext").GetComponentInChildren<Animator>();
        aud = FindObjectOfType<AudioManager>();
        gm = FindObjectOfType<GameManager>();

        playerInRange = false;
    }

    private void OnMouseEnter()
    {
        text.text = ObjectiveParentName;
        StartCoroutine("TextFadeIn");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.enabled = true;
            playerInRange = true;
        }
    }

    private void OnMouseOver()
    {
        if (playerInRange)
        {
            if (ObjectivesToComplete >= Objectives.Length && hasItem && Input.GetKeyDown(KeyCode.E))
            {
                ObjectiveComplete();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerInRange = false;
        StartCoroutine("TextFadeOut");
    }

    private void OnMouseExit()
    {
        StartCoroutine("TextFadeOut");
    }

    public void ObjectiveComplete()
    {
        //check for item in inventory
        text.enabled = true;        //Objective complete
        text.text = ObjectiveCompleteText;        //activate UI
        aud.PlaySound("ObjectiveParent");//play sound
        GetComponentInChildren<ParticleSystem>().Play();//play particle effect?
        StartCoroutine("ObjectiveTimer"); // timer to allow the particle system to play
        if (IsLevelObjective == true)
        {
            gm.LevelComplete();
        }
    }

    public IEnumerator ObjectiveTimer()
    {
        yield return new WaitForSeconds(ObjectiveTimerWait);
    }

    public IEnumerator TextFadeIn()
    {
        text.enabled = true;
        textAnim.SetBool("fadeText", true);
        yield return new WaitForSeconds(textFadeTime);
    }

    public IEnumerator TextFadeOut()
    {
        textAnim.SetBool("fadeText", false);
        yield return new WaitForSeconds(textFadeTime);
        text.enabled = false;
    }
}
