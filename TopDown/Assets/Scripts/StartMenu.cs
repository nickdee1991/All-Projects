using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public Button startGame;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene("Opening");
        Debug.Log("Application Loading");
    }

    public void Training()
    {
        SceneManager.LoadScene("Training");
        Debug.Log("Loading Training");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
