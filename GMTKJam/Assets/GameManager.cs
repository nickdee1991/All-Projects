using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool hasFinished;
    public bool hasDied;
    public GameObject FinishUI;

    public float time;
    private TMPro.TextMeshProUGUI timerText;

    public int RandomSound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine("RestartLevel");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        time += Time.deltaTime;
        timerText.text = "GZTF37 12/07/1962 " + time.ToString();

        //Debug.Log(time);
    }

    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.Find("Timer").GetComponent<TMPro.TextMeshProUGUI>();
        hasFinished = false;
        hasDied = false;
    }

    public IEnumerator LevelComplete()
    {
        if (hasDied != true)
        {
            hasFinished = true;
            FinishUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().enabled = true;
            FinishUI.GetComponentInChildren<RawImage>().enabled = true;
            FinishUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Glory to sneaky bot and to the Zagorgkzkiy Republic. Do next test now move next camera now hurry comrade!";
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
    IEnumerator RestartLevel()
    {
        if (hasFinished != true)
        {
            hasDied = true;
            FinishUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().enabled = true;
            FinishUI.GetComponentInChildren<RawImage>().enabled = true;
            FinishUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "You bring shame to the glorious republic of Zagorgkzkiy";
            FindObjectOfType<AudioManager>().PlaySound("Restart");
            GetComponent<RandomSoundManager>().StartCoroutine("InteractingSound");
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
