using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager1 : MonoBehaviour {

    public Animator cameraAnimator;
    public Animator monitorAnimator;

    private bool startGame;

    // Use this for initialization
    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        if (sceneName == "Start")
        {
            startGame = true;
        }
        else if (sceneName == "Opening")
        {
            // Do something...
        }

        // Retrieve the index of the scene in the project's build settings.
        int buildIndex = currentScene.buildIndex;

        // Check the scene name as a conditional.
        switch (buildIndex)
        {
            case 0:
                // Do something...
                break;
            case 1:
                // Do something...
                break;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.E) && startGame == true)
        {
            cameraAnimator.SetBool("CameraMotion", true);
            monitorAnimator.SetBool("LoadLevel", true);
            StartCoroutine(StartGame());
        }
	}

    IEnumerator StartGame()
    {
        print("loading level");
        yield return new WaitForSeconds(3);
        startGame = false;
        //Application.LoadLevel("Level1");
        SceneManager.LoadScene("Opening");
    }
}
