using UnityEngine;
using System.Collections;

public class NumberWizard : MonoBehaviour {

	int max;
	int min;
	int guess;

	// Use this for initialization
	void Start () 
	{
		StartGame();
	}

	void StartGame() 
	{
		max = 1000;
		min = 1;
		guess = 500;


		print ("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
		print ("Welcome to number wizard");
		print ("Pick a number in yo muhfucken head but don't tell my ass, bitch");
		
		
		
		print ("the highest number you can pick is " + max);
		print ("the lowest number you can pick is " + min);
		
		print ("is the number higher or lower than " + guess);
		print ("up = higher, down = lower, return = equals");

		max = max + 1;

	}


	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.UpArrow))
		{
			//print("Up arrow was pressed");
			min = guess;
			NextGuess();
		} else if (Input.GetKeyDown (KeyCode.DownArrow))
		{
			//print("Down arrow was pressed");
			max = guess;
			NextGuess();
		} else if (Input.GetKeyDown (KeyCode.Return))
		{
			print("I won");
			StartGame();
		}

	}

	void NextGuess ()
	{
		guess = (max + min) / 2;
		print ("Higher or lower than " + guess);
		print ("up = higher, down = lower, return = equals");
	}
}
