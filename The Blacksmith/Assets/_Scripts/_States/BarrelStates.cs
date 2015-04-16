using UnityEngine;
using System.Collections;

public class BarrelStates : MonoBehaviour {

	public GameObject Player { get; set; }
	public string State { get; set; }
	public string WeaponState { get; set; }

	private GameObject weapon;

	private double cooldown = .5;
	private bool isDipped = false;
	private bool position = false;

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
	}
	
	// Update is called once per frame
	void Update () 
	{
		cooldown += Time.deltaTime;
		if (Player != null) 
		{
			if(weapon != null){
				if(!position){
					position=true;
					WeaponPosition();
				}
				WeaponState = weapon.GetComponent<ItemStates>().State;
				//Debug.Log(weapon.tag + ": " + WeaponState);
			}
		}
		else 
		{
			weapon = null;
			WeaponState = "NULL_STATE"; // if Player disattaches themselves reset the weapon info to null
			position = false;
		}
		//James' Logic block.  Again.
		if(Input.GetMouseButton(1)){
			if(weapon != null){
				if(!isDipped){
					WeaponToward();	
					isDipped = true;				
				}
				if(cooldown >= .5){
					weapon.GetComponent<ItemStates>().addHeat(-15);
					cooldown = 0;
				}
			}
		}
		else{
			if(isDipped){
				WeaponAway();
				isDipped = false;
			}
		}
	}	
	public void WeaponPosition(){
		if(weapon != null){
			Snap();
			weapon.transform.Rotate (new Vector3(0,0,180));
			weapon.transform.localPosition = new Vector3(0f, 1f, 1f);
		}
	}
	void WeaponToward(){
		if(weapon != null){
			Snap();
			weapon.transform.localPosition = new Vector3(0f, .2f, 1f);
		}
	}	
	void WeaponAway(){
		if(weapon != null){
			Snap();
			weapon.transform.localPosition = new Vector3(0f, 1f, 1f);
		}
	}	
	void Snap(){
		Player.transform.position = new Vector3(transform.position.x + 1, Player.transform.position.y, transform.position.z);
		Player.transform.rotation = (Player.transform.rotation*(Quaternion.FromToRotation (Player.transform.forward, transform.forward)));
		Player.transform.Rotate (new Vector3(0, -90, 0));
	}
}
