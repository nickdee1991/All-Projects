using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour 
{

	public Text text;
	private enum States {intro, cell, mirror, sheets_0, lock_0, cell_mirror, sheets_1, lock_1, corridor_0, stairs_0};
	private States myState;

	// Use this for initialization
	void Start () 
	{
		myState = States.intro;
	}
	
	// Update is called once per frame
	void Update () 
	{
		print (myState);
		if (myState == States.intro) {state_intro ();} 

		else if (myState == States.cell) 			{state_cell ();} 
		else if (myState == States.cell_mirror) 	{state_cell_mirror ();} 
		else if (myState == States.mirror) 			{state_mirror ();}
		else if (myState == States.lock_0) 			{state_lock_0 ();} 
		else if (myState == States.lock_1) 			{state_lock_1 ();}
		else if (myState == States.sheets_0) 		{state_sheets_0 ();} 		 
		else if (myState == States.sheets_1) 		{state_sheets_1 ();} 
		else if (myState == States.corridor_0) 		{state_corridor_0 ();}
		else if (myState == States.stairs_0) 		{state_stairs_0 ();}
	}
			
	void state_intro ()

		{
			text.text = 
				  "You are a criminal on the run for stealing some diamonds from a local jewelery store." 
				+ " You used the money from the score to rent all 3 Shrek movies on DVD, the late fee was no object. \n\n" 
				+ "You were content with your existence, and your worries were but a distant memory. \n\n" 
				+ "Life was good... \n \n"
				+ "Sadly, all good things come to an end, they found your filthy cheeto-stained prints at the crime scene. \n \n" 
				+ "You were trialed \n"
				+ "Found guilty \n" 
				+ "And are now standing in your cell clad with a jumpsuit of the same colour as the cheese-flavoured snack that put you here. \n" 
				+ "Press Y for 'You fucked up!' to continue.";

			if (Input.GetKeyDown (KeyCode.Y)) 		{myState = States.cell;}
		}


	void state_cell ()
		{
			text.text = "It's time to get out! \n" +
				"There are some dirty sheets on the bed," +
				" a mirror on the wall, \n" +
				"and the door is locked from the outside. \n \n" +
				"Press S to view Sheets. \n" +
				"M to view the Mirror. \n" +
				"L to view the Lock.";

			if (Input.GetKeyDown (KeyCode.S))		{myState = States.sheets_0;}

			if (Input.GetKeyDown (KeyCode.M))		{myState = States.mirror;}

			if (Input.GetKeyDown (KeyCode.L))		{myState = States.lock_0;}

		}
	void state_sheets_0 ()
	{

		text.text = "The sheets are full of crusty jizz, reminds you of home...\n\n" 
			+ "Press R to return.";
		if (Input.GetKeyDown (KeyCode.R)) 		{myState = States.cell;}
	}

	void state_sheets_1 ()
	{
		
		text.text = "You take another look at the sheets, a wee nap would do the trick right about now. \n" 
			+ "But you know the longer you're locked up here the higher chance you have of getting a shiv in the kidneys.\n \n"
			+ "Press R to return.";
		if (Input.GetKeyDown (KeyCode.R)) 		{myState = States.cell_mirror;}
	}

	void state_mirror ()
	{
			
		text.text = "A little hand mirror, its been a while since you looked at yourself in one of these. \nYou have a maniacal grin on your face. \n\n"
				+ "Perhaps you belong here. \n \n"
				+ "Press T to take the mirror. \n" 
				+ "Press R to return.";
		if (Input.GetKeyDown (KeyCode.R)) 		{myState = States.cell;}
		else if (Input.GetKeyDown (KeyCode.T)) 		{myState = States.cell_mirror;}
		
	}

	void state_cell_mirror ()
	{
		
		text.text = "You're back in the middle of your cell holding the mirror in your hand. \n\n"
				+ "Those crusty sheets are still there. \n" 
				+ "And that stubborn door is still playing silly buggers with you.\n\n"
				+ "Press S to view sheets, or L to view lock.";
		if (Input.GetKeyDown (KeyCode.S)) 		{myState = States.sheets_1;}
		else if (Input.GetKeyDown (KeyCode.L)) 		{myState = States.lock_1;}
		
	}

	void state_lock_0 ()
	{
		
		text.text = "You examine the lock, it's one of those button locks that needs a specific keycode activate! \n" 
			+ "No chance of getting through this with brute force, you'll need to find something in the room to help open it.\n\n"
			+ "Press R to return.";
		if (Input.GetKeyDown (KeyCode.R)) 		{myState = States.cell;}
	}

	void state_lock_1 ()
	{
		
		text.text = "You carefully put the mirror through the bars and turn it around so you can see the lock. \n" 
				+ "You can faintly make out some wear on 4 of the buttons. \n" 
				+ "You experiment pressing the buttons in different orders until...\n\n" 
				+ "Success! you heard a faint buzzing coming from the lock!\n \n"
				+ "Press R to return, or O to open the cell door.";
		if (Input.GetKeyDown (KeyCode.R)) 		{myState = States.cell_mirror;}
		if (Input.GetKeyDown (KeyCode.O)) 		{myState = States.corridor_0;}
	}

	void state_corridor_0 ()
	{
		
		text.text = "The cell door opens with a rattle, a calming breeze wisps through the hallway and cools the sweat on your brow. \n \n"
			+ "You are free... \n \n";	
		if (Input.GetKeyDown (KeyCode.S)) 		{myState = States.stairs_0;}

	}

	void state_stairs_0 ()

	{
		text.text =  "3 burly white men in orange jumpsuits hear you creep up the stairs and beat you with pillowcases full of old potatoes.\n"				
				+ "Press R to escape.";
			if (Input.GetKeyDown (KeyCode.R)) 		{myState = States.corridor_0;}
	}	
}

