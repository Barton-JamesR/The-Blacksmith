using UnityEngine;
using System.Collections;

public class AnvilStates : MonoBehaviour {

	public GameObject Player { get; set; }
	public AudioClip clip;
	public string State { get; set; }
	public string WeaponState { get; set; }
	public string ToolState { get; set; }
	private bool isPoised = false;//James: the sword being poised to strike.
	private double cooldown = 2.0;//James: two seconds between strikes, to ensure no rapid-fire forging.
	private double hammercd = 1.5;
	private bool down = false;
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
			cooldown += Time.deltaTime;
			hammercd -= Time.deltaTime;
			if (weapon != null)
			if(weapon.tag == "Weapon" || weapon.tag=="Iron" || weapon.tag=="Steel")
			{
				WeaponState = weapon.GetComponent<ItemStates>().State;
				if(down){
					if(hammercd <= 0){
						ToolAway();
					}
				}
				//Debug.Log(weapon.tag + ": " + WeaponState);
			}
			if (tool != null)
			{
				ToolState = tool.GetComponent<ItemStates>().State;
				//Debug.Log(tool.tag + ": " + ToolState);
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

		//Beyond this point is the stuff James put into the same script file for convenience's sake; it'll be moved afterward (probably).
		if(Input.GetMouseButton(1)){
			//Debug.Log("Weapon is:" + weapon);
			if(weapon != null)
			if(weapon.tag == "Iron" || weapon.tag == "Steel" || weapon.tag == "Weapon"){
				if(!isPoised){
					isPoised = true;
					WeaponToward();
					//The weapon's optimal position is .56, -.5, 1
				}
				//DebugLog("Poised to strike!");
			}
		}
		else{
			if(isPoised){
				isPoised = false;
				if(weapon != null){
					//DebugLog("Weapon's present form: " + weapon.GetComponent<ItemStates>().Form);
					WeaponAway ();
				}
			}
		}
		if(Input.GetMouseButton(0)){
			if(isPoised){
				if(tool != null){
					if(tool.tag == "Hammer"){
						ToolToward();
						if(!down){
							down = true;
							audio.Stop ();
							audio.clip=clip;
							audio.Play ();
							if(weapon.GetComponent<ItemStates>().Form < 100){
								if(weapon.GetComponent<ItemStates>().Heat >= 50){
									weapon.GetComponent<ItemStates>().addForm(5);
									weapon.GetComponent<ItemStates>().addQuality(2);
									cooldown = 0;
								}
								else{
									weapon.GetComponent<ItemStates>().addQuality(-4);
									weapon.GetComponent<ItemStates>().addDurability(-5);
									cooldown = 0;
								}
							}
						}
					}
				}
			}
		}
		else{
			ToolAway();
			down = false;
		}
	}
	void WeaponToward(){
		if(weapon != null){
			Snap ();
			weapon.transform.Rotate (new Vector3(0,0,90));
			weapon.transform.localPosition = new Vector3(0.56f, -.5f, 1f);
		}
		if(tool != null){
			Snap ();
			tool.transform.localPosition = new Vector3(0, .39f, .55f);
		}
	}	
	void WeaponAway(){
		if(weapon != null){
			Snap ();
			weapon.transform.Rotate (new Vector3(0,0,-90));
			weapon.transform.localPosition = new Vector3(1f, 0f, 1f);
		}
		if(tool != null){
			Snap ();
			tool.transform.localPosition = new Vector3(-1,0,1);
		}
	}
	void ToolToward(){
		if(tool != null){
			Snap ();
			tool.transform.localPosition = new Vector3(0, -.39f, .6f);
		}
	}
	void ToolAway(){
		if(tool != null){
			Snap ();
			tool.transform.localPosition = new Vector3(0, .39f, .55f);
		}
	}
	void Snap(){
		Player.transform.position = new Vector3(transform.position.x + 1, Player.transform.position.y, transform.position.z);
		Player.transform.rotation = (Player.transform.rotation*(Quaternion.FromToRotation (Player.transform.forward, transform.forward)));
		Player.transform.Rotate (new Vector3(0, -90, 0));
	}
}

//TO DO AFTER LUNCH
//Barrel Mechanics
//Grindstone mechanics