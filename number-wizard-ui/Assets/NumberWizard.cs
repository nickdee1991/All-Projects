using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NumberWizard : MonoBehaviour {

	int max;
	int min;
	int guess;
	public int maxGuessesAllowed = 10;

	public Text text;

	// Use this for initialization
	void Start () 
	{
		StartGame();
	}

	void StartGame() 
	{
		max = 1000;
		min = 1;
		NextGuess ();

	}

	public void GuessLower()
	{
		max = guess;
		NextGuess();
	}


	public void GuessHigher()
	{
		min = guess;
		NextGuess();
	}

	void NextGuess ()
	{
		guess = (Random.Range (max+1 , min));
		text.text = guess.ToString();
		maxGuessesAllowed = maxGuessesAllowed - 1;
		if (maxGuessesAllowed <= 0)
		{
			Application.LoadLevel("Win");
		}
	}
}
