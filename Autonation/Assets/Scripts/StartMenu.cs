using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public Button startGame;
    public AudioSource Aud;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        Aud.Play();
        SceneManager.LoadScene("Opening");
        Debug.Log("Application Loading");
    }

    public void Training()
    {
        Aud.Play();
        SceneManager.LoadScene("Training");
        Debug.Log("Loading Training");
    }

    public void Quit()
    {
        Aud.Play();
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
