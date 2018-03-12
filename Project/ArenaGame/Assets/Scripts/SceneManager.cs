using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class SceneManager : MonoBehaviour {

	public void LoadLevel(string name)
	{
        Debug.Log ("Level load requested for: " + name);
        //LoadLevel("Woorld");
        Application.LoadLevel(name);
    }

	public void QuitRequest(string name)
	{
		Debug.Log ("Level quit requested for: " + name);
		Application.Quit ();
	}
}
