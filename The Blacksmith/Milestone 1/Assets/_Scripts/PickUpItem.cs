using UnityEngine;
using System.Collections;

public class PickUpItem : MonoBehaviour {
	// Declare Variables
	public GameObject myItem;

	void Start()
	{
		PickUp(myItem);
	}

	// The PickUp Function
	// Purpose: 	Pick up a game object and place it in either the right or left hand.
	//				Also places it as a child of the player in the unity hierarchy.
	// Parameters: 	A reference to the GameObject to be picked up. The GameObject will not be picked up if it doesn't have an acceptable tag.
	// Returns: 	None
	void PickUp(GameObject myItem)
	{
		// constants for the correct item positioning
		Vector3 LEFT_HAND_POS = new Vector3(-.45f, .45f, .55f);
		Vector3 RIGHT_HAND_POS = new Vector3(.45f, .45f, .55f);

		// constant arrays holding the acceptable list of tags whose items may be picked up.
		string[] RH_TAG_LIST = new string[] {"Weapon", "Tool"};
		string[] LH_TAG_LIST = new string[] {"Shield", "Bar"};

		bool leftHand = false;
		bool rightHand = false;

		// See whether this is a left or right hand item
		for (int i = 0; i < RH_TAG_LIST.Length; i++)
		{
			if (myItem.tag == RH_TAG_LIST[i])
			{
				rightHand = true;
			}
		}
		for (int i = 0; i < LH_TAG_LIST.Length; i++)
		{
			if (myItem.tag == LH_TAG_LIST[i])
			{
				leftHand = true;
			}
		}

		if(rightHand) // Put myItem into the right hand
		{
			// Make myItem a child of the player
			myItem.transform.parent = transform;

			// Put the item in the right spot relative to the player
			myItem.transform.position = transform.position + RIGHT_HAND_POS;

			// Rotate the item to its held rotation
			myItem.transform.rotation = Quaternion.Euler(30,0,0); 
		}
		else if(leftHand) // Put myItem into the left hand
		{
			// Make myItem a child of the player
			myItem.transform.parent = transform;

			// Put the item in the right spot relative to the player
			myItem.transform.position = transform.position + LEFT_HAND_POS;

			// Rotate the item so it looks like it is being held properly
			myItem.transform.rotation = Quaternion.Euler(30,0,0);
		}
	} // End PickUp()
} // End PickUpItem
