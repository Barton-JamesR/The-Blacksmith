using UnityEngine;
using System.Collections;

public class ForgeStates : MonoBehaviour {

	public GameObject Player { get; set; }
	public float Temperature { get; set; }
	public string State { get; set; }
	public string WeaponState { get; set; }

	private GameObject weapon;

	public void SetWeapon(GameObject w)
	{
		weapon = w;
	}

	// Use this for initialization
	void Start () 
	{
		Player = null;
		Temperature = 800f;
		State = "NULL_STATE";
		WeaponState = "NULL_STATE";
	}
	
	// Update is called once per frame
	void Update () 
	{
		// put the code in here for anything that we want to do while at the Forge.

		if (Player != null) 
		{
			weapon = GameObject.FindWithTag("Weapon");
			WeaponState = weapon.GetComponent<ItemStates>().State;
			Debug.Log(weapon.tag + ": " + WeaponState);
		}
		else 
		{
			weapon = null;
			WeaponState = "NULL_STATE"; // if Player disattaches themselves reset the weapon info to null
		}
	}
}
