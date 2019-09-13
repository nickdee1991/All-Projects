using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypeWriterEffectEpilogue : MonoBehaviour {

    public float delay = 0.1f;
    private string fullText = "You  have  stolen  the  data  from  Monument  Platinum  and  delivered  it  to  our  employer \n \n Funds  have  been  transferred  to  your  account \n  We  will  keep  these  channels  open  should  you  wish  to  earn  more \n \n all  sounds  from  freesounds.org & www.zapsplat.com  -  Music  by  Eric  Matyas  soundimage.org \n  Music: Level1:  Dark-Techno-City \n Level2:  The-Secret-Lab \n Level3:  And-the-Machines-Came-at-Midnight \n Menu:  World-of-Automatons \n \n You  have  finished  _Autonation                press  e  to  restart  application ";
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
            SceneManager.LoadScene("Menu");
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
