using UnityEngine;
using System.Collections;

public class AnvilStates : MonoBehaviour {

	public GameObject Player { get; set; }
	public string State { get; set; }
	public string WeaponState { get; set; }
	public string ToolState { get; set; }

	private GameObject weapon;
	private GameObject tool;

	public void SetWeapon(GameObject myWeapon)
	{
		weapon = myWeapon;
	}
	public void SetTool(GameObject myTool)
	{
		tool = myTool;
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
		if (Player != null) 
		{
			if (weapon != null)
			{
				WeaponState = weapon.GetComponent<ItemStates>().State;
				Debug.Log(weapon.tag + ": " + WeaponState);
			}
			if (tool != null)
			{
				ToolState = tool.GetComponent<ItemStates>().State;
				Debug.Log(tool.tag + ": " + ToolState);
			}
		}
		else 
		{
			weapon = null;
			WeaponState = "NULL_STATE"; // if Player disattaches themselves reset the weapon info to null

			tool = null;
			ToolState = "NULL_STATE";
			//Debug.Log("AnvilState Player was set to null");
		}
	}
}
