using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseOverHighlighter : MonoBehaviour{

    private Color startcolor;
    public GameObject floatingText;
    //public GameObject highlighter;

    void OnMouseEnter()
    {
        floatingText.gameObject.SetActive(true);
        startcolor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.green;
        //Instantiate(highlighter, transform.position, Quaternion.identity);
    }

    void OnMouseExit()
    {
        floatingText.gameObject.SetActive(false);
        GetComponent<Renderer>().material.color = startcolor;
    }
}
