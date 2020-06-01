using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHolder : MonoBehaviour
{
    public bool textStart;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        textStart = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (textStart)
        {
            StartCoroutine(StartText());
        }   
    }
     public IEnumerator StartText()
    {
        player.GetComponent<Player>().movementSpeed = 0;
        if (!textStart)
        {
            textStart = true;
        }
        yield return new WaitForSeconds(1f);      //disable player speed for a bit
        player.GetComponent<Player>().movementSpeed = 7.5f;
        yield return new WaitForSeconds(3);     //display text for seconds then disable
        GetComponent<Text>().enabled = false;
        textStart = false;
    }
}
