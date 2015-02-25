using UnityEngine;
using System.Collections;

public class WorkbenchStates : MonoBehaviour {

	public GameObject Player { get; set; }
	public string State { get; set; } // a property for holding a state ... at the moment not sure exactly what it'll be but if we need it it's here
	public string WeaponState { get; set; } // maybe we will need a property that holds the state of the weapon we are working with.
	public string ToolState { get; set; } // maybe another property for the state of the tool we are working with.

	private GameObject weapon;
	private GameObject tool;

	public void SetWeapon(GameObject w)
	{
		weapon = w;
	}
	public void SetTool(GameObject t)
	{
		tool = t;
	}

	// Use this for initialization
	void Start () 
	{
		Player = null;
		State = "NULL_STATE";
		WeaponState = "NULL_STATE";
		ToolState = "NULL_STATE";
	}
	
	// Update is called once per frame
	void Update () 
	{
		// put the code in here for anything that we want to do while at the Workbench.

		if (Player != null) 
		{
			// constantly check and update the state of the weapon
			weapon = GameObject.FindWithTag("Weapon");
			WeaponState = weapon.GetComponent<ItemStates>().State;
			Debug.Log(weapon.tag + ": " + WeaponState);

			tool = GameObject.FindWithTag("Hammer");
			ToolState = tool.GetComponent<ItemStates>().State;
			Debug.Log(tool.tag + ": " + ToolState);
		}
		else 
		{
			weapon = null;
			WeaponState = "NULL_STATE"; // if Player disattaches themselves reset the weapon info to null

			tool = null;
			ToolState = "NULL_STATE";
		}
	}
}
