using UnityEngine;
using System.Collections;

public class ForgeStates : MonoBehaviour {

	public GameObject Player { get; set; }
	public float Temperature { get; set; }
	public string State { get; set; }
	public string WeaponState { get; set; }
	private GameObject weapon;

	public bool isBurning = false;

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
			if(weapon != null){
				WeaponState = weapon.GetComponent<ItemStates>().State;
				//Debug.Log(weapon.GetComponent<ItemStates>().Heat);
				//Debug.Log(weapon);
			}
		}
		else 
		{
			weapon = null;
			WeaponState = "NULL_STATE"; // if Player disattaches themselves reset the weapon info to null
		}

		if(Input.GetMouseButton(1)){
			if(weapon != null){
				if(!isBurning){
					isBurning = true;
					AnimateTowards ();
					//The sword should look to be the following:  x0, y.3, z.5, rotation x90, y0, z0, w0.
				}
				//Debug.Log("In the forge");
				weapon.GetComponent<ItemStates>().addHeat(1);
			}
		}
		else{
			if(isBurning){
				AnimateAway ();
				isBurning = false;
			}
		}
	}
	void AnimateTowards(){
		if(weapon != null){
			Snap();
			//weapon.transform.rotation = Quaternion.RotateTowards (weapon.transform.rotation, new Quaternion(90, 0, 0, 0), Time.deltaTime);
			weapon.transform.Rotate (new Vector3(90, 0, 0));
			//weapon.transform.position = Vector3.MoveTowards (weapon.transform.position, new Vector3(0f, .3f, .5f), Time.deltaTime);
			weapon.transform.localPosition = new Vector3(0f, .3f, .5f);
		}
	}
	void AnimateAway(){
		if(weapon != null){
			Snap();
			weapon.transform.Rotate (new Vector3(-90,0,0));
			weapon.transform.localPosition = new Vector3(1f, 0f, 1f);
		}
	}	
	void Snap(){
		Player.transform.position = new Vector3(transform.position.x + 1, Player.transform.position.y, transform.position.z);
		Player.transform.rotation = (Player.transform.rotation*(Quaternion.FromToRotation (Player.transform.forward, transform.forward)));
		Player.transform.Rotate (new Vector3(0, -90, 0));
	}

}
