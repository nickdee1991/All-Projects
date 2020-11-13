using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI speechText;
    public float CutsceneTime;
    public float SpeechTime;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Cursor.visible = false;
        }
    }

    IEnumerator ActivateSpeech()
    {
        //activate text componennt on player and change text depending on trigger
        //maybe play sound - deactivate after below timer
        speechText.enabled = true;
        //speechText.text = "text is working";
        yield return new WaitForSeconds(SpeechTime);
        speechText.enabled = false;
    }

    IEnumerator FinishCutscene()
    {
        Debug.Log("FinishCutscene timer started");
        yield return new WaitForSeconds(.25f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
