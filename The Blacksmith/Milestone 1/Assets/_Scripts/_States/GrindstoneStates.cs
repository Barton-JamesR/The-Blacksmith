using UnityEngine;
using System.Collections;

public class GrindstoneStates : MonoBehaviour {

	public GameObject Player { get; set; }
	public string State { get; set; }
	public string WeaponState { get; set; }
	public int WeaponSharpness { get; set; }

	private GameObject weapon;

	public void SetWeapon(GameObject w)
	{
		weapon = w;
	}

	// Use this for initialization
	void Start () 
	{
		Player = null;
		State = "NULL_STATE";
		WeaponState = "NULL_STATE";
		WeaponSharpness = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Player != null) 
		{
			weapon = GameObject.FindWithTag("Weapon");
			WeaponState = weapon.GetComponent<ItemStates>().State;
			WeaponSharpness = weapon.GetComponent<ItemStates>().Sharpness;
			Debug.Log(weapon.tag + ": " + WeaponState);
		}
		else 
		{
			weapon = null;
			WeaponState = "NULL_STATE"; // if Player disattaches themselves reset the weapon info to null
			WeaponSharpness = 0;
		}
	}
}
