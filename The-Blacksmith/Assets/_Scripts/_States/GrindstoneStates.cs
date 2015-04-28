using UnityEngine;
using System.Collections;

public class GrindstoneStates : MonoBehaviour {

	public GameObject Player { get; set; }
	public AudioClip clip;
	public string State { get; set; }
	public string WeaponState { get; set; }
	public int WeaponSharpness { get; set; }

	private GameObject weapon;

	private double cooldown = .2;
	private bool isSet = false;

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
		cooldown += Time.deltaTime;
		if (Player != null) 
		{
			if(weapon != null){
				WeaponState = weapon.GetComponent<ItemStates>().State;
				WeaponSharpness = weapon.GetComponent<ItemStates>().Sharpness;
				//Debug.Log(weapon.tag + ": " + WeaponState);
			}
		}
		else 
		{
			weapon = null;
			WeaponState = "NULL_STATE"; // if Player disattaches themselves reset the weapon info to null
			WeaponSharpness = 0;
		}
		if(Input.GetMouseButton(1)){
			if(weapon != null){
				if(!isSet){
					audio.clip = clip;
					audio.Play ();
					isSet = true;
					WeaponToward();
				}
				if(cooldown >= .2){
					cooldown = 0;
					if(weapon.GetComponent<ItemStates>().Form == 100){
						weapon.GetComponent<ItemStates>().addSharpness(2);
						weapon.GetComponent<ItemStates>().addQuality (1);
					}
					if(weapon.GetComponent<ItemStates>().Sharpness >= 100)
						weapon.GetComponent<ItemStates>().addQuality (-1);
				}
			}
		}
		else{
			if(isSet){
				WeaponAway();
				audio.Stop ();
				isSet = false;
			}
		}
	}
	void WeaponToward(){
		if(weapon != null){
			Snap();
			weapon.transform.Rotate (new Vector3(0,0,70));
			weapon.transform.localPosition = new Vector3(0.63f, .15f, 1f);
		}
	}	
	void WeaponAway(){
		if(weapon != null){
			Snap();
			weapon.transform.Rotate (new Vector3(0,0,-70));
			weapon.transform.localPosition = new Vector3(1f, 0f, 1f);
		}
	}	
	void Snap(){
		Player.transform.position = new Vector3(transform.position.x + 1, Player.transform.position.y, transform.position.z);
		Player.transform.rotation = (Player.transform.rotation*(Quaternion.FromToRotation (Player.transform.forward, transform.forward)));
		Player.transform.Rotate (new Vector3(0, -90, 0));
	}
}
