using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypeWriterEffect : MonoBehaviour {

    public float delay = 0.1f;
    private string fullText = "Hey  hacker,  thanks  for  taking  this  job  on  such  short  notice \n simple  gig  just  like  the  others \n  break   into  the  building,  get  to  the  server  room  and  retrieve  the  files \n  \n oh  and  find  a  way  out,  you  may  have  to  improvise! \n \n \n  WASD  to  move       SPACE  to  jump       E  to  Interact       Mouse2  +  1  to  shock/ flashlight   \n  Press  e  to  start ";
    private string currentText = "";
    private AudioSource Aud;

    private void Start()
    {
        Aud = GetComponent<AudioSource>();
        StartCoroutine(ShowText());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Level1");
        }
    }

    IEnumerator ShowText()
    {
        for(int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);            
        }
        yield return null;
        Aud.Stop();
    }
}
