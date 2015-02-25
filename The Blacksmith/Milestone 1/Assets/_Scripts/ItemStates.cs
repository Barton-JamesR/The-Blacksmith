using UnityEngine;
using System.Collections;

public class ItemStates : MonoBehaviour 
{
	// Properties that can store the State of the GameObject and the index in the array of STATES
	public string State { get; set; }
	public int StateIndex { get; set; }

	// A constant string that contains a set list of all the possible states
	string[] STATES = {"Good", "Bad"};
	
	//public enum STATES{GOOD, BAD}
	//enum myState = GOOD;

	// Reference to this script
	private ItemStates myItemState;

	// Variables for debugging purposes
	private string tempState = "";
	private float clickTime = 0f;

	// Use this for initialization
	void Start () 
	{
		// Initialize the index and the state
		StateIndex = 0;
		State = STATES[StateIndex];

		// Get the component reference to this script
		myItemState = GetComponent <ItemStates>();
	} // End Start()

	// Update is called once per frame
	void Update()
	{
		// Test to see if the states can change
		if (Input.GetButton("Fire1") && Time.time > clickTime)
		{
			tempState = State;
			StateIndex = (StateIndex + 1) % STATES.Length;
			State = STATES[StateIndex];

			// Display the new and old states, for debugging purposes only
			guiText.text = string.Format("Old State: {0} \nNew State: {1}", tempState, State);

			clickTime = .5f + Time.time;
		} 
	} // End Update()
} // End ItemStates