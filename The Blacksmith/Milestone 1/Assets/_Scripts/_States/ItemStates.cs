using UnityEngine;
using System.Collections;

public class ItemStates : MonoBehaviour 
{
	// Properties that can store the State of the GameObject and the index in the array of STATES
	public string State { get; set; }
	public int Quality { get; set; }	// To hold a numerical representation of the quality of the item
	public int Durability { get; set; }	// To hold a numerical representation of the durability of the item
	public int Sharpness { get; set; }

	// Note that we can use the weapons Durability as way of checking progress on the creation of the item ... or maybe just another property
	// public int Progress { get; set; }

	// A constant string that contains a set list of all the possible states
	string[] STATES = {"Good", "Bad"};
	
	//public enum STATES{GOOD, BAD}
	//enum myState = GOOD;

	// Reference to this script
	private ItemStates myItemState;

	// Use this for initialization
	void Start () 
	{
		State = STATES[0]; // initialize the state of an item to "Good"
		Quality = 100;
		Durability = 100; // 100 sounds good for these ... 
		Sharpness = 0;
	} // End Start()
} // End ItemStates