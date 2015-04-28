using UnityEngine;
using System.Collections;

public class PlayerStates : MonoBehaviour {
	
	public string Place { get; set; } // holds the place that the player is at (i.e. Anvil)
	public string State { get; set; } // holds the state of the player ... 
	public string LeftHand { get; set; } // holds the tag of the object that the player is holding in the left hand.
	public string RightHand { get; set; } // holds the tag of the object that the player is holding in the right hand.
	public int points { get; set; }

	// These may not really get used much but they are a list of the strings that can be assigned to the public properties
	string[] PLACES = {"Free","Anvil", "Forge", "Workbench", "Barrel", "Grindstone"};
	string[] STATES = {"Healthy", "Hurt", "Dead"};
	string[] LEFT = {"Empty","Hammer"};
	string[] RIGHT = {"Empty", "Weapon", "Iron"};

	void Start()
	{
		Place = PLACES[0]; // the player starts, unattached to any object
		State = STATES[0]; // the player starts healthy
		LeftHand = LEFT[0]; // start off holding nothing
		RightHand = RIGHT[0]; // start off holding nothing
		points = 0; //start off with 0 points
		Screen.showCursor = false;
	} // End Start()
	void addPoints(int pts){
		points += pts;
	}
}
