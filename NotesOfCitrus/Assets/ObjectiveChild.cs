using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveChild : MonoBehaviour
{
    private GameObject player;
    //public Transform ObjectiveDisabled;
    private GameManager gm;
    public string ObjectName;
    public ObjectiveParent Parent;
    public Animator ObjectiveAnim; // an animation to play when objchild is brought to parent
    private TextMeshProUGUI text;
    private Animator textAnim;

    private bool hasPickedUp;
    private bool playerInRange;

    public float textFadeTime = .5f;
    private int destPoint;
    public Transform[] spawnPoints;


    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        text = GameObject.FindGameObjectWithTag("UItext").GetComponentInChildren<TextMeshProUGUI>();
        textAnim = GameObject.FindGameObjectWithTag("UItext").GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //destPoint is assigned a random number = to the length of the spawnpoint array
        destPoint = (Random.Range(0, spawnPoints.Length));

        //Instantiate item in random location on start
        if(spawnPoints.Length != 0)
        {
            transform.position = spawnPoints[destPoint].position;
        }

        hasPickedUp = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.enabled = true;
            playerInRange = true;
        }
    }

    private void OnMouseEnter()
    {
        Debug.Log(ObjectName);
        text.text = ObjectName;
        StartCoroutine("TextFadeIn");
    }

    private void OnMouseOver()
    {
        if (playerInRange && hasPickedUp == false)
        {
            if (Input.GetKeyDown(KeyCode.E) && hasPickedUp == false)
            {
                if (ObjectiveAnim != null)
                {
                    ObjectiveAnim.SetBool("ObjectiveAnim", true);//add optional animation to play
                }
                hasPickedUp = true;
                Parent.hasItem = true; //update in gamemanager
                Parent.ObjectivesToComplete++;
                gameObject.GetComponent<MeshRenderer>().enabled = false;//pickup this object
                gameObject.transform.position = new Vector3(0, -5, 0);//pickup this object
                text.text = ObjectName + " picked up"; //text = " picked up"
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
        playerInRange = false;
        StartCoroutine("TextFadeOut");
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
