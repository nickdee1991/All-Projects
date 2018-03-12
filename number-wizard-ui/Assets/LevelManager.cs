using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {



	public void LoadLevel(string name)
	{
		Debug.Log ("Level load requested for: " + name);
		Application.LoadLevel (name);
	}

	public void QuitRequest(string name)
	{
		Debug.Log ("Level quit requested for: " + name);
		Application.Quit ();
	}
}
